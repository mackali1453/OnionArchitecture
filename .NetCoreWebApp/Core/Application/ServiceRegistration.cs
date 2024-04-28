using Application.CQRS.Commands;
using Application.CQRS.Handlers.User;
using Application.CQRS.Handlers.Vehicle;
using Application.CQRS.Queries;
using Application.Dto;
using Application.Interfaces;
using Application.Mappers;
using Application.Services;
using AutoMapper;
using Github.NetCoreWebApp.Core.Application.Services;
using Github.NetCoreWebApp.Core.Applications.Dto;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Github.NetCoreWebApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new UserMapper());
                opt.AddProfile(new VehicleMapper());
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IUserService),
                typeof(UserService));
            services.AddScoped(typeof(IVehicleService),
                typeof(VehicleService));

            services.AddTransient<IRequestHandler<LoginQueryRequest, LoginResponseDto>, LoginQueryRequestHandler>();
            services.AddTransient<IRequestHandler<UserCreateCommandRequest, UserResponseDto>, UserCreateCommandHandler>();
            services.AddTransient<IRequestHandler<UserDeleteCommandRequest, Unit>, UserDeleteCommandHandler>();
            services.AddTransient<IRequestHandler<UserUpdateCommandRequest, UserResponseDto>, UserUpdateCommandHandler>();
            services.AddTransient<IRequestHandler<UserGetQueryRequest, UserResponseDto>, UserGetQueryHandler>();

            services.AddTransient<IRequestHandler<VehicleCreateCommandRequest, VehicleResponseDto>, VehicleCreateCommandHandler>();
            services.AddTransient<IRequestHandler<VehicleDeleteCommandRequest, Unit>, VehicleDeleteCommandHandler>();
            services.AddTransient<IRequestHandler<VehicleUpdateCommandRequest, VehicleResponseDto>, VehicleUpdateCommandHandler>();
            services.AddTransient<IRequestHandler<VehicleGetQueryRequest, VehicleResponseDto>, VehicleGetQueryHandler>();
        }
    }
}