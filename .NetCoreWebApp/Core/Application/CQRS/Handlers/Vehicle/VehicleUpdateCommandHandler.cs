using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Vehicle
{
    public class VehicleUpdateCommandHandler : IRequestHandler<VehicleUpdateCommandRequest, VehicleResponseDto>
    {
        private readonly IVehicleService _vehicleService;

        public VehicleUpdateCommandHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<VehicleResponseDto> Handle(VehicleUpdateCommandRequest updateVehicleRequest, CancellationToken cancellationToken)
        {
            return await _vehicleService.Update(updateVehicleRequest);
        }
    }
}
