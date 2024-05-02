using Github.NetCoreWebApp.Presentation.Filters;
using Github.NetCoreWebApp.Presentation.Middlewares;

namespace Github.NetCoreWebApp.Presentation
{
    public static class ServiceRegistration
    {
        public static void AddMiddlewareDependencies(this IServiceCollection builder)
        {
            builder.AddScoped<ValidateModelFilter>();
            builder.AddScoped<RequestResponseLoggingMiddleware>();
        }
    }
}
