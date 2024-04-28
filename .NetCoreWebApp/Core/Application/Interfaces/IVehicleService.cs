using Application.CQRS.Commands;
using Application.Dto;

namespace Application.Interfaces
{
    public interface IVehicleService  : IBaseService<VehicleResponseDto, VehicleCreateCommandRequest, VehicleUpdateCommandRequest>
    {
    }
}
