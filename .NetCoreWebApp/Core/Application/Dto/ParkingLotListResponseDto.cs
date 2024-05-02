namespace Application.Dto
{
    using System.Collections.Generic;

    public class ParkingLotListResponseDto : ApiResponse<List<ParkingLotData>>
    {
        public ParkingLotListResponseDto(bool success, string message, List<ParkingLotData> data)
            : base(success, message, data)
        {
        }
    }

}
