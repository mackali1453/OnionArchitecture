using Domain.Entities;

namespace Github.NetCoreWebApp.Core.Domain.Entities
{
    public class AppUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? MobilePhoneNumber { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public Int64? Tckn { get; set; }
        public string? Gender { get; set; }
        public bool IsLockedOut { get; set; }
        public int LogInTryCount { get; set; }
        public DateTime LockedUpExpireDate { get; set; }
        public List<AppUserRole> AppUserRole { get; set; }
        public List<Product> AppUserProducts { get; set; }
    }
}
