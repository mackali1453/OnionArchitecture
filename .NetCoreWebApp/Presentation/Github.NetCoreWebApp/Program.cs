using Github.NetCoreWebApp.Core.Application;
using Github.NetCoreWebApp.Core.Domain.Entities;
using Github.NetCoreWebApp.Infrastructure.Common;
using Github.NetCoreWebApp.Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Github.NetCoreWebApp.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidAudience = "https://localhost",
        ValidIssuer = "https://localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(".NetCoreOnionArchitectur")),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddApplicationDependencies();
builder.Services.AddOptions();
builder.Services.Configure<Logging>(builder.Configuration.GetSection("Logging"));
builder.Services.AddCommonDependencies();
builder.Services.AddMiddlewareDependencies();
builder.Services.AddPersistanceDependencies(builder.Configuration.GetConnectionString("Local"));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
