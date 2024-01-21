using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Core.Domain.Entities;
using Github.NetCoreWebApp.Infrastructure.Persistance.Context;
using Github.NetCoreWebApp.Infrastructure.Repositories;

namespace Github.NetCoreWebApp.Infrastructure.Persistance.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly WebApiContext _context;
        private readonly LogContext _logContext;

        public Uow(WebApiContext context)
        {
            _context = context;
        }
        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_context);
        }
        public IRepository<T> GetLogRepository<T>() where T : class, new()
        {
            return new Repository<T>(_logContext);
        }
        public IUserRepository<AppUser> GetUserRepository()
        {
            return new UserRepository<AppUser>(_context);
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
        public async Task SaveLogChanges()
        {
            await _logContext.SaveChangesAsync();
        }
    }
}
