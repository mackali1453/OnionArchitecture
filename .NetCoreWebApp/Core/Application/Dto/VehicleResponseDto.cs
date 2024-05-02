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
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }
        public string VehiclePlate { get; set; }
        public string IsActive { get; set; }

        public VehicleData()
        {
        }
        public VehicleData(int id, string vehicleBrand, string vehicleModel, string vehicleColor, string vehiclePlate)
        {
            Id = id;
            VehicleBrand = vehicleBrand;
            VehicleModel = vehicleModel;
            VehicleColor = vehicleColor;
            VehiclePlate = vehiclePlate;
        }
    }
}
