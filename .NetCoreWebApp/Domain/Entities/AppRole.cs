using Domain.Entities;

namespace Github.NetCoreWebApp.Core.Domain.Entities
{
    public class AppRoles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<AppUserRole> AppUserRoles { get; set; }
    }
}
