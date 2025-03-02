using Application.Contracts.Email;
using Application.Models.EmailModel;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfig _emailConfig;
        private readonly ILogger<EmailSender> _logger;
        public EmailSender(IOptions<EmailConfig> emailConfig, ILogger<EmailSender> logger)
        {
            _emailConfig = emailConfig.Value;
            _logger = logger;
        }
        public async Task<bool> SendEmailAsync(EmailMessage email)
        {
            return true;
        }
    }
}
