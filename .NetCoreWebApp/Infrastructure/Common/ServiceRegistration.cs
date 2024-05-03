using Common.Interfaces;
using Github.NetCoreWebApp.Infrastructure.Common.Helpers;
using Github.NetCoreWebApp.Infrastructure.Common.Interfaces;
using Github.NetCoreWebApp.Infrastructure.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Github.NetCoreWebApp.Infrastructure.Common
{
    public static class ServiceRegistration
    {
        public static void AddCommonDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IServiceLogger<>), typeof(ServiceLogger<>));
            services.AddSingleton<IUtility, Utility>();

            var jsonFilePath = configuration.GetSection("AppSettings:filePaths:logFilePath").Value;
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(jsonFilePath, optional: true, reloadOnChange: true);

            services.Configure<LogSettings>(configurationBuilder.Build().GetSection("LogSettings"));
        }
    }
}
