using AutoMapper;
using Github.NetCoreWebApp.Core.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Github.NetCoreWebApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new ProductProfile());
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
