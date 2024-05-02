
using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.Dto;

namespace Application.Interfaces
{
    public interface IParkingLotService : IBaseService<ParkingLotResponseDto, object, ParkingLotUpdateCommandRequest>
    {
        Task<ParkingLotListResponseDto> GetCoordinatesInsideCircle(ParkingLotCoordinatesInsideCircleRequest request);
    }
}
