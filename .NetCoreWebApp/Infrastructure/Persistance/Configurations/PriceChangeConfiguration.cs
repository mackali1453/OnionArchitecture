using Domain.Entities.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class PriceChangeConfiguration : IEntityTypeConfiguration<PriceChange>
    {
        public void Configure(EntityTypeBuilder<PriceChange> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Product).WithMany(x => x.PriceChange);
        }
    }
}
