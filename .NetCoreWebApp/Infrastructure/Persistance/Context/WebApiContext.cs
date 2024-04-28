using Domain.Entities.Aggregates;
using Microsoft.EntityFrameworkCore;
using Persistance.Configurations;

namespace Github.NetCoreWebApp.Infrastructure.Persistance.Context
{
    public class WebApiContext : DbContext
    {
        public DbSet<AppUser> AppUsers => this.Set<AppUser>();
        public DbSet<AppVehicle> AppVehicle => this.Set<AppVehicle>();
        public DbSet<ParkingLot> ParkingLot => this.Set<ParkingLot>();

        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleConfigurations());
            modelBuilder.ApplyConfiguration(new ParkingLotConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
