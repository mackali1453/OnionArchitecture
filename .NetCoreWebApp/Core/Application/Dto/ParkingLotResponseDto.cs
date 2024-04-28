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
        public ParkingLotData() { }
    }
}
