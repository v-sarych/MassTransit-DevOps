using Microsoft.AspNetCore.Mvc;

namespace RefitTest.Controllers
{
    [ApiController]
    [Route("")]
    public class PingController : ControllerBase
    {
        private readonly IConsumerApi _consumerApi;
        public PingController(IConsumerApi consumerApi)
            => _consumerApi = consumerApi;

        [HttpGet("GetSenderPong")]
        public async Task<string> GetSenderPong(string message)
        {
                return await _consumerApi.GetPong(message);
        }
    }
}
