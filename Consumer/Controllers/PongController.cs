using Microsoft.AspNetCore.Mvc;

namespace Consumer.Controllers
{
    [ApiController]
    [Route("")]
    public class PongController : ControllerBase
    {
        [HttpGet("GetPong")]
        public async Task<string> GetPong([FromQuery]string message)
        {
            if (message.ToLower() == "ping")
                return "pong";
            else
                return "no ping";
        }
    }
}
