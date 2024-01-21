using Github.NetCoreWebApp.Core.Domain.Entities;

namespace Domain.Entities
{
    public class AppUserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public AppUser AppUser { get; set; }
        public AppRoles AppRoles { get; set; }
    }
}
