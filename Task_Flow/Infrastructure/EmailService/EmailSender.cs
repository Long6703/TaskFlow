using Application.Contracts.Email;
using Application.Models.EmailModel;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IOptions<EmailSettings> emailSettings, ILogger<EmailSender> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }
        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            _logger.LogInformation("Email sending...: {recipient}", emailMessage.To);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(emailMessage.To));
            message.Subject = emailMessage.Subject;

            var builder = new BodyBuilder();
            if (emailMessage.IsHtml)
            {
                builder.HtmlBody = emailMessage.Body;
            }
            else
            {
                builder.TextBody = emailMessage.Body;
            }

            if (emailMessage.Attachments != null)
            {
                foreach (var attachment in emailMessage.Attachments)
                {
                    builder.Attachments.Add(attachment.FileName, attachment.Content, ContentType.Parse(attachment.ContentType));
                }
            }

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort,
                    _emailSettings.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);

                await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            _logger.LogInformation("Email sending successfut to: {recipient}", emailMessage.To);
        }
    }
}
