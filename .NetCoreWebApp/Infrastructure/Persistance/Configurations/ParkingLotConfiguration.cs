using Domain.Entities.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class ParkingLotConfiguration : IEntityTypeConfiguration<ParkingLot>
    {
        public void Configure(EntityTypeBuilder<ParkingLot> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.AppUser).WithOne(x => x.ParkingLot).HasForeignKey<ParkingLot>(x => x.AppUserId);
        }
    }
}
