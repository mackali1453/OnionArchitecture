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

        private readonly List<AppVehicle> _appVehicle = new List<AppVehicle>();
        public IReadOnlyCollection<AppVehicle> AppVehicle => _appVehicle.AsReadOnly();


        private ParkingLot _parkingLot;
        public ParkingLot ParkingLot => _parkingLot;
        public AppUser() { }

        public AppUser(string name, string surname, string mobilePhoneNumber,
                       string userName, string password, long tckn, string gender)
        {
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
        public void SetRelationWithVehicle(AppVehicle vehicle)
        {
            _appVehicle.Add(vehicle);
        }
        public void SetRelationWithParkingLot(ParkingLot parkingLot)
        {
            _parkingLot = parkingLot;
        }
    }
}
