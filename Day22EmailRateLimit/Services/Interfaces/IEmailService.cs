using Day22EmailRateLimit.DTO;

namespace Day22EmailRateLimit.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequestDto request);
    }
}