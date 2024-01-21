using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Github.NetCoreWebApp.Infrastructure.Persistance.Configurations.UserConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasKey(x => x.UserRoleId);

            builder.HasData(new AppUserRole[] {
                new() {UserRoleId=1,RoleId=1,UserId=1},
                new() {UserRoleId=2,RoleId=2,UserId=1},
                new() {UserRoleId=3,RoleId=3,UserId=1}
            });
            builder.HasOne(x => x.AppRoles).WithMany(x => x.AppUserRoles).HasForeignKey(x => x.RoleId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.AppUserRole).HasForeignKey(x => x.UserId);
        }
    }
}
