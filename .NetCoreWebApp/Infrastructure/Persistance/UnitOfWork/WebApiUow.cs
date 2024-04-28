using Application.Interfaces;
using Domain.Entities.Aggregates;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Infrastructure.Persistance.Context;
using Github.NetCoreWebApp.Infrastructure.Repositories;
using Persistance.Migrations;

namespace Github.NetCoreWebApp.Infrastructure.Persistance.UnitOfWork
{
    public class WebApiUow : IWebApiIuow
    {
        private readonly WebApiContext _webApiContext;
        public WebApiUow(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_webApiContext);
        }
        public async Task SaveChangesAsync()
        {
            await _webApiContext.SaveChangesAsync();
        }
    }
}
