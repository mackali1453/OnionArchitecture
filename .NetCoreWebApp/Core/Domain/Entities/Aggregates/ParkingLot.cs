using Domain.Interfaces;

namespace Domain.Entities.Aggregates
{
    public class ParkingLot : BaseEntity, IAggregateRoot
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        private AppUser _appUser = new AppUser();
        public AppUser AppUser => _appUser;
        public int AppUserId { get;private set; }
        public ParkingLot(double latitude, double longitude, int id = 0)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }

        public ParkingLot()
        {
        }
        public void AddUser(int userID)
        {
            AppUserId = userID;
        }
        //public void SetRelationWithUser(AppUser user)
        //{
        //    _appUser = user;
        //}
    }
}
