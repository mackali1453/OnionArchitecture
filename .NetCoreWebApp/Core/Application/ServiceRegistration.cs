using Application.CQRS.Commands;
using Application.CQRS.Handlers;
using Application.CQRS.Handlers.User;
using Application.CQRS.Queries;
using Application.Dto;
using Application.Interfaces;
using Application.Mappers;
using Application.Services;
using Application.ValidationRules;
using AutoMapper;
using Common.Interfaces;
using FluentValidation;
using Github.NetCoreWebApp.Core.Application.Services;
using Github.NetCoreWebApp.Core.Application.ValidationRules;
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
                opt.AddProfile(new ProductMapper());
                opt.AddProfile(new UserMapper());
            });
            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILogService, LogService>();

            services.AddTransient<IValidator<ProductCreateCommandRequest>, ProductCreateValidator>();
            services.AddTransient<IValidator<ProductGetQueryRequest>, ProductGetValidator>();
            services.AddTransient<IValidator<ProductUpdateCommandRequest>, ProductUpdateValidator>();
            services.AddTransient<IValidator<UserCreateCommandRequest>, UserCreateValidator>();

            services.AddTransient<IRequestHandler<LoginQueryRequest, LoginResponseDto>, LoginQueryRequestHandler>();
            services.AddTransient<IRequestHandler<ProductCreateCommandRequest, ProductResponseDto>, ProductCreateCommandHandler>();
            services.AddTransient<IRequestHandler<ProductGetQueryRequest, ProductResponseDto>, ProductGetQueryHandler>();
            services.AddTransient<IRequestHandler<ProductUpdateCommandRequest, ProductResponseDto>, ProductUpdateCommandHandler>();
            services.AddTransient<IRequestHandler<UserCreateCommandRequest, UserResponseDto>, UserCreateCommandHandler>();
        }
    }
}