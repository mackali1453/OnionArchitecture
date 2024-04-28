using Domain.Entities.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class VehicleConfigurations : IEntityTypeConfiguration<AppVehicle>
    {
        public void Configure(EntityTypeBuilder<AppVehicle> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.AppUser).WithMany(x => x.AppVehicle);
        }
    }
}
