using Github.NetCoreWebApp.Core.Domain.Entities;

namespace Github.NetCoreWebApp.Core.Applications.Interfaces
{
    public interface IUserRepository<T> : IRepository<T> where T : AppUser, new()
    {
        public AppUser GetEagerUsers(int id);
        public AppUser GetEagerUsers(string username);
    }
}
