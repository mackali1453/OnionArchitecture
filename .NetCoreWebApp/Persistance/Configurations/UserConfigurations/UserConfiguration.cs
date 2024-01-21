using Github.NetCoreWebApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Github.NetCoreWebApp.Infrastructure.Persistance.Configurations.UserConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x=>x.UserId);
            builder.HasData(new AppUser[] {
                new() {UserId=1,Gender="Traversti",Name="Götü",Surname="Fermuarlı"}
            });
        }
    }
}
