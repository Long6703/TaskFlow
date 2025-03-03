using Application.Contracts.Email;
using Application.Models.EmailModel;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Channels;

namespace Infrastructure.EmailService
{
    public class EmailServiceImp : IEmailService
    {
        private readonly ILogger<EmailServiceImp> _logger;
        private readonly Channel<EmailMessage> _emailQueue;
        public EmailServiceImp(ILogger<EmailServiceImp> logger, Channel<EmailMessage> emailQueue)
        {
            _logger = logger;
            _emailQueue = emailQueue;
        }

        public async Task QueueEmailAsync(EmailMessage emailMessage)
        {
            await _emailQueue.Writer.WriteAsync(emailMessage);
        }

        public async Task QueueEmailAsync(string to, string subject, string body, bool isHtml = true)
        {
            var emailMessage = new EmailMessage
            {
                To = to,
                Subject = subject,
                Body = body,
                IsHtml = isHtml
            };

            await QueueEmailAsync(emailMessage);
        }
    }
}
