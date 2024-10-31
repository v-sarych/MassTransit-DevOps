using Core;
using MassTransit;

namespace Consumer
{
    public class MessageDConsumer : IConsumer<MessageD>
    {

        private static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private static QueueMessageHandler? _queueMessageHandler;
        public static event QueueMessageHandler? QueueMessageHandler
        {
            add
            {
                _lock.EnterWriteLock();
                try
                {
                    if (_queueMessageHandler == null)
                        _queueMessageHandler = new QueueMessageHandler(value);
                    else
                        _queueMessageHandler += value;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
            remove
            {
                _lock.EnterWriteLock();
                try
                {
                    if (_queueMessageHandler != null)
                        _queueMessageHandler -= value;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }


        public Task Consume(ConsumeContext<MessageD> context)
        {
            _lock.EnterReadLock();
            try
            {
                _queueMessageHandler?.Invoke(context.Message);
            }
            finally
            {
                _lock.ExitReadLock();
            }

            return Task.CompletedTask;
        }
    }
}
