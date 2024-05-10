using Domain.Interfaces;

namespace Domain.Entities.Aggregates
{
    public class AppUser : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string MobilePhoneNumber { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public long Tckn { get; private set; }
        public string Gender { get; private set; }
        public bool IsLockedOut { get; private set; }
        public int LogInTryCount { get; private set; }
        public DateTime LockedUpExpireDate { get; private set; }

        public AppUser() { }

        public AppUser(string name, string surname, string mobilePhoneNumber,
                       string userName, string password, long tckn, string gender, int id = 0)
        {
            Id = id;
            Name = name;
            Surname = surname;
            MobilePhoneNumber = mobilePhoneNumber;
            UserName = userName;
            Password = password;
            Tckn = tckn;
            Gender = gender;
            IsLockedOut = false;
            LogInTryCount = 0;
            LockedUpExpireDate = DateTime.MinValue;
        }
    }
}
