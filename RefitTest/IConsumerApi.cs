using Refit;

namespace RefitTest
{
    public interface IConsumerApi
    {
        [Get("/GetPong")]
        Task<string> GetPong([Query]string message);
    }
}
