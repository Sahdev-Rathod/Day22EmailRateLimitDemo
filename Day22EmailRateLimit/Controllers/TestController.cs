using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Day22EmailRateLimit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("data")]
        [EnableRateLimiting("fixed")]
        public IActionResult GetData()
        {
            return Ok(new
            {
                message = "API request successful"
            });
        }
    }
}