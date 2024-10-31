using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using Core;

namespace Consumer.Controllers
{
    [ApiController]
    [Route("")]
    public class ConsumeController : ControllerBase
    {
        private WebSocket ws;

        [HttpGet()]
        public async Task CreateWebSocket()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                ws = await HttpContext.WebSockets.AcceptWebSocketAsync();

                MessageDConsumer.QueueMessageHandler += _messageHandler;
                Console.WriteLine("Connection Opened");

                await _receiver(ws);

                MessageDConsumer.QueueMessageHandler -= _messageHandler;
            }
            else
            {
                Console.WriteLine("not web socket connection");
            }

        }//*/

        private async Task _messageHandler(MessageD messageD)
        {
            if (ws.State == WebSocketState.Open)
            {
                var bytes = Encoding.UTF8.GetBytes(messageD.Message);
                var data = new ArraySegment<byte>(bytes, 0, bytes.Length);
                await ws.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        private async Task _receiver(WebSocket ws)
        {
            while (ws.State == WebSocketState.Open)
            {
                var buffer = new byte[1024];
                var data = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            Console.WriteLine("Connection Closed");
        }
    }
}
