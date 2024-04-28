namespace Application.Dto
{
    public class VehicleResponseDto : ApiResponse<VehicleData>
    {
        public VehicleResponseDto(bool success, string message, VehicleData data) : base(success, message, data)
        {
        }

    }
    public class VehicleData
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }
        public string VehiclePlate { get; set; }

        public VehicleData(int ıd, int userID, string vehicleBrand, string vehicleModel, string vehicleColor, string vehiclePlate)
        {
            Id = ıd;
            UserID = userID;
            VehicleBrand = vehicleBrand;
            VehicleModel = vehicleModel;
            VehicleColor = vehicleColor;
            VehiclePlate = vehiclePlate;
        }
    }
}
