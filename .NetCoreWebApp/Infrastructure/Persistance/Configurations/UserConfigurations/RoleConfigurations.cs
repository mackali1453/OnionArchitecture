using Github.NetCoreWebApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Github.NetCoreWebApp.Infrastructure.Persistance.Configurations.UserConfigurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<AppRoles>
    {
        public void Configure(EntityTypeBuilder<AppRoles> builder)
        {
            builder.HasKey(x => x.RoleId);

            builder.HasData(new AppRoles[] {
                new() {RoleId=1, RoleName="Admin" },
                new() {RoleId=2,RoleName="Agent" },
                new() {RoleId=3,RoleName="Customer" },
                new() {RoleId=4,RoleName="Api" }
            });
        }
    }
}
