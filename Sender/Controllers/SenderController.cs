using Core;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Sender.Controllers
{
    [ApiController]
    [Route("")]
    public class SenderController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public SenderController(IPublishEndpoint publishEndpoint) 
            => _publishEndpoint = publishEndpoint;

        [HttpPost("Send")]
        public async Task Send(string message)
        {
            await _publishEndpoint.Publish(new MessageD()
            {
                Message = message
            });
        }
    }
}
