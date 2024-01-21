using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Core.Domain.Entities;
using Github.NetCoreWebApp.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Github.NetCoreWebApp.Infrastructure.Repositories
{
    public class UserRepository<T> : Repository<T>, IUserRepository<T> where T : AppUser, new()
    {
        public WebApiContext _wepApiContext;

        public UserRepository(WebApiContext wepApiContext) : base(wepApiContext)
        {
            _wepApiContext = wepApiContext;
        }

        public Task CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByFilter(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public AppUser GetEagerUsers(int id)
        {
            //.AppUserRole.Select(x => x.AppRoles).ToList()
            var user = _wepApiContext.AppUsers.Include(x => x.AppUserRole).ThenInclude(x => x.AppRoles).SingleOrDefault(x => x.UserId == id);
            return user;
        }
        public AppUser GetEagerUsers(string username)
        {
            var user = _wepApiContext.AppUsers.Include(x => x.AppUserRole).ThenInclude(x => x.AppRoles).SingleOrDefault(x => x.UserName == username);
            return user;
        }

        public Task RemoveAsyn(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T updatedEntity, T oldEntity)
        {
            throw new NotImplementedException();
        }
    }
}
