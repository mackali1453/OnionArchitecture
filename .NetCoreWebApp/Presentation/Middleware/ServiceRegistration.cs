using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Presentation.Middleware.Logging;
using Github.NetCoreWebApp.Presentation.Middleware.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Github.NetCoreWebApp.Presentation.Middleware
{
    public static class ServiceRegistration
    {
        public static void AddMiddlewareDependencies(this IServiceCollection builder)
        {
            builder.AddScoped<ValidateModelAttribute>();
            builder.AddScoped<LoggingAttribute>();
        }
    }
}
