using Domain.Interfaces;

namespace Domain.Entities.Aggregates
{
    public class ParkingLot : BaseEntity, IAggregateRoot
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        private AppUser _appUser = new AppUser();
        public AppUser AppUser => _appUser;
        public int AppUserId { get; set; }
        public ParkingLot(long latitude, long longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public ParkingLot()
        {
        }

        public void SetRelationWithUser(AppUser user)
        {
            _appUser = user;
        }
    }
}
