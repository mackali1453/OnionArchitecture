using Github.NetCoreWebApp.Core.Domain.Entities;

namespace Github.NetCoreWebApp.Core.Applications.Interfaces
{
    public interface IUow
    {
        IRepository<T> GetRepository<T>() where T : class, new();
        public IRepository<T> GetLogRepository<T>() where T : class, new();
        public IUserRepository<AppUser> GetUserRepository();
        Task SaveChanges();
        Task SaveLogChanges();

    }
}
