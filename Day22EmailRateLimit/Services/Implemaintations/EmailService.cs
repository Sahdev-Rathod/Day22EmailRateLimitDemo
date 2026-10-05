using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Day22EmailRateLimit.DTO;
using Day22EmailRateLimit.Services.Interfaces;

namespace Day22EmailRateLimit.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailRequestDto request)
        {
            var email = new MimeMessage();

            email.From.Add(
                new MailboxAddress(
                    "Day22 Application",
                    _configuration["EmailSettings:Email"]
                )
            );

            email.To.Add(
                MailboxAddress.Parse(request.To)
            );

            email.Subject = request.Subject;

            var bodyBuilder = new BodyBuilder
            {
                TextBody = request.Body
            };

            if (request.Attachment != null)
            {
                using var stream = new MemoryStream();

                await request.Attachment.CopyToAsync(stream);

                bodyBuilder.Attachments.Add(
                    request.Attachment.FileName,
                    stream.ToArray(),
                    ContentType.Parse(request.Attachment.ContentType)
                );
            }

            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _configuration["EmailSettings:SmtpServer"],
                int.Parse(_configuration["EmailSettings:Port"]),
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                _configuration["EmailSettings:Email"],
                _configuration["EmailSettings:Password"]
            );

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);
        }
    }
}