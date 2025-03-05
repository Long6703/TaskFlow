using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddService();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

        public static void AddService(this IServiceCollection services)
        {
            #region Registration repository
            var assembly = Assembly.GetAssembly(typeof(ApplicationServiceRegistration));
            var classes = assembly.ExportedTypes
               .Where(a => !a.Name.StartsWith("I") && a.Name.EndsWith("Service"));
            foreach (Type implement in classes)
            {
                foreach (var @interface in implement.GetInterfaces())
                {
                    services.AddScoped(@interface, implement);
                }
            }
            #endregion
        }
    }
}
