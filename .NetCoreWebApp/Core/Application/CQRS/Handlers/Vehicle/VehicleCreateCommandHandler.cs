using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Vehicle
{
    public class VehicleCreateCommandHandler : IRequestHandler<VehicleCreateCommandRequest, VehicleResponseDto>
    {
        private readonly IVehicleService _vehicleService;

        public VehicleCreateCommandHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<VehicleResponseDto> Handle(VehicleCreateCommandRequest request, CancellationToken cancellationToken)
        {
            return await _vehicleService.Create(request);
        }
    }
}
