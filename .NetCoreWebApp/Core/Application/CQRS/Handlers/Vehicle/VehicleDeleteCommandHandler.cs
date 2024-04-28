using Application.CQRS.Commands;
using Application.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Vehicle
{
    public class VehicleDeleteCommandHandler : IRequestHandler<VehicleDeleteCommandRequest, Unit>
    {
        private readonly IVehicleService _vehicleService;

        public VehicleDeleteCommandHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<Unit> Handle(VehicleDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            await _vehicleService.Delete(request.Id);

            return Unit.Value;
        }
    }
}
