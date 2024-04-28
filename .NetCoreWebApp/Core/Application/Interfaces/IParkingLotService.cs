
using Application.CQRS.Commands;
using Application.Dto;

namespace Application.Interfaces
{
    public interface IParkingLotService : IBaseService<ParkingLotResponseDto, ParkingLotCreateCommandRequest, ParkingLotUpdateCommandRequest>
    {
    }
}
