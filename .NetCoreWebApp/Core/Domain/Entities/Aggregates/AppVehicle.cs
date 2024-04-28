using Domain.Interfaces;

namespace Domain.Entities.Aggregates
{
    public class AppVehicle : BaseEntity, IAggregateRoot
    {
        public string VehicleBrand { get; private set; }
        public string VehicleModel { get; private set; }
        public string VehicleColor { get; private set; }
        public string VehiclePlate { get; private set; }
        public bool IsActive { get; private set; }

        private AppUser _appUser = new AppUser();
        public AppUser AppUser { get { return _appUser; } }

        public AppVehicle(string vehiclePlate, string vehicleColor, string vehicleModel, string vehicleBrand, bool ısActive)
        {
            VehiclePlate = vehiclePlate;
            VehicleColor = vehicleColor;
            VehicleModel = vehicleModel;
            VehicleBrand = vehicleBrand;
            IsActive = ısActive;
        }
        public AppVehicle()
        {
        }

        public void SetRelationWithUser(AppUser user)
        {
            _appUser = user;
        }
    }
}
