using Application.CQRS.Queries;
using Application.Dto;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Vehicle
{
    public class VehicleGetQueryHandler : IRequestHandler<VehicleGetQueryRequest, VehicleResponseDto>
    {
        private readonly IVehicleService _vehicleService;

        public VehicleGetQueryHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<VehicleResponseDto> Handle(VehicleGetQueryRequest request, CancellationToken cancellationToken)
        {
            return await _vehicleService.GetById(request.Id);
        }
    }
}
