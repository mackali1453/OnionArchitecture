using Application.Interfaces;
using Domain.Entities;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Infrastructure.Persistance.Context;
using Github.NetCoreWebApp.Infrastructure.Repositories;

namespace Github.NetCoreWebApp.Infrastructure.Persistance.UnitOfWork
{
    public class LoggerIuow : ILoggerIuow
    {
        private readonly LogContext _logContext;

        public LoggerIuow(LogContext logContext)
        {
            _logContext = logContext;
        }

        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_logContext);
        }

        public async Task SaveChangesAsync()
        {
            await _logContext.SaveChangesAsync();
        }
    }
}
