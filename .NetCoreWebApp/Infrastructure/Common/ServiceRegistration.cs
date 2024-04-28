using Application.Interfaces;
using Github.NetCoreWebApp.Core.Application.Interfaces;
using Github.NetCoreWebApp.Infrastructure.Common.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Github.NetCoreWebApp.Infrastructure.Common
{
    public static class ServiceRegistration
    {
        public static void AddCommonDependencies(this IServiceCollection builder)
        {
            builder.AddScoped(typeof(IServiceLogger<>), typeof(ServiceLogger<>));
            builder.AddSingleton<IUtility, Utility>();
        }
    }
}
