using Day22EmailRateLimit.DTO;
using Day22EmailRateLimit.Services;
using Day22EmailRateLimit.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Day22EmailRateLimit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail( [FromForm] EmailRequestDto request)
        {
            await _emailService.SendEmailAsync(request);

            return Ok(new
            {
                message = "Email sent successfully"
            });
        }
    }
}