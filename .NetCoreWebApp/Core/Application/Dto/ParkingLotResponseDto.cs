namespace Application.Dto
{
    public class ParkingLotResponseDto : ApiResponse<ParkingLotData>
    {
        public ParkingLotResponseDto(bool success, string message, ParkingLotData data) : base(success, message, data)
        {
        }
    }

    public class ParkingLotData
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public ParkingLotData() { }
        public ParkingLotData(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
