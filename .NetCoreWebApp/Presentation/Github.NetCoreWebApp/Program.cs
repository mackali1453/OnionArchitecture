using Domain.Entities;
using Github.NetCoreWebApp.Core.Application;
using Github.NetCoreWebApp.Filters;
using Github.NetCoreWebApp.Infrastructure.Common;
using Github.NetCoreWebApp.Infrastructure.Persistance;
using Github.NetCoreWebApp.Presentation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidAudience = builder.Configuration.GetSection("AppSettings:JwtSettings:Issuer").Value,
        ValidIssuer = builder.Configuration.GetSection("AppSettings:JwtSettings:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:JwtSettings:SecretKey").Value)),
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.OperationFilter<AddAuthorizationHeaderOperationFilter>();
});
builder.Services.AddOptions();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddPersistanceDependencies(builder.Configuration.GetSection("AppSettings:ConnectionStrings:Local").Value);
builder.Services.AddApplicationDependencies();
builder.Services.AddCommonDependencies(builder.Configuration);
builder.Services.AddMiddlewareDependencies();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.MapControllers();
app.Run();
