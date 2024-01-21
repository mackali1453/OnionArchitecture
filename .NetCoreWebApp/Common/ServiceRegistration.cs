using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Infrastructure.Common.DbLogger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Github.NetCoreWebApp.Infrastructure.Common
{
    public static class ServiceRegistration
    {
        public static void AddCommonDependencies(this IServiceCollection builder)
        {
            builder.AddScoped(typeof(IServiceLogger<>), typeof(ServiceLogger<>));           
        }
    }
}
