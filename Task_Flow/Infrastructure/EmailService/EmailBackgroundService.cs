using Application.Contracts.Email;
using Application.Models.EmailModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infrastructure.EmailService
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly Channel<EmailMessage> _emailQueue;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EmailBackgroundService> _logger;

        public EmailBackgroundService(Channel<EmailMessage> emailQueue, IServiceProvider serviceProvider, ILogger<EmailBackgroundService> logger)
        {
            _emailQueue = emailQueue;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var emailMessage in _emailQueue.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                    await emailSender.SendEmailAsync(emailMessage);
                    _logger.LogInformation("Email send successful to: {recipient}", emailMessage.To);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error send email to {recipient}", emailMessage.To);
                }
            }
        }
    }
}
