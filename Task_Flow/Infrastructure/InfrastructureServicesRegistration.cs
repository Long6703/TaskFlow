using Application.Contracts.Email;
using Application.Contracts.Logging;
using Application.Models.EmailModel;
using Infrastructure.DatabaseContext;
using Infrastructure.EmailService;
using Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;

namespace Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskFlowContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            var emailChannel = Channel.CreateUnbounded<EmailMessage>(new UnboundedChannelOptions
            {
                SingleReader = true,
                SingleWriter = false
            });
            services.AddSingleton(emailChannel);
            services.AddTransient<IEmailService, EmailServiceImp>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddHostedService<EmailBackgroundService>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            return services;
        }
    }
}
