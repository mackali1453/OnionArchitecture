using Domain.Entities;
using Github.NetCoreWebApp.Core.Domain.Entities;
using Github.NetCoreWebApp.Infrastructure.Persistance.Configurations;
using Github.NetCoreWebApp.Infrastructure.Persistance.Configurations.UserConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Github.NetCoreWebApp.Infrastructure.Persistance.Context
{
    public class WebApiContext : DbContext
    {
        public DbSet<Product> Products => this.Set<Product>();
        public DbSet<Category> Categories => this.Set<Category>();
        public DbSet<AppUser> AppUsers => this.Set<AppUser>();
        public DbSet<AppRoles> AppRoles => this.Set<AppRoles>();
        public DbSet<AppUserRole> AppUserRole => this.Set<AppUserRole>();

        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
