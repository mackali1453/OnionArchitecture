using Domain.Entities.Aggregates;
using Microsoft.EntityFrameworkCore;
using Persistance.Configurations;

namespace Github.NetCoreWebApp.Infrastructure.Persistance.Context
{
    public class WebApiContext : DbContext
    {
        public DbSet<PriceChange> PriceChange => this.Set<PriceChange>();
        public DbSet<Product> Product => this.Set<Product>();

        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PriceChangeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfigurations());
            base.OnModelCreating(modelBuilder);
        }
    }
}
