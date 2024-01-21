using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Infrastructure.Persistance.Context;
using Github.NetCoreWebApp.Infrastructure.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Github.NetCoreWebApp.Infrastructure.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceDependencies(this IServiceCollection builder,string connectionString)
        {
            builder.AddDbContext<WebApiContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
                //opt.LogTo(Console.WriteLine, LogLevel.Information);
            }
            );
            builder.AddDbContext<LogContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
                //opt.LogTo(Console.WriteLine, LogLevel.Information);
            }
            );
            builder.AddScoped<IUow, Uow>();
        }
    }
}
