using Application.CQRS.Commands;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;
using MediatR;

namespace Application.CQRS.Handlers.Vehicle
{
    public class VehicleDeleteCommandHandler : IRequestHandler<VehicleDeleteCommandRequest, Unit>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleDeleteCommandHandler(IWebApiIuow unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Unit> Handle(VehicleDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicleRepository = _unitOfWork.GetRepository<AppVehicle>();
                var vehicle = await vehicleRepository.GetByIdAsync(request.Id);

                if (vehicle != null)
                {
                    vehicleRepository.Remove(vehicle);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete vehicle.", ex);
            }
            return Unit.Value;
        }
    }
}
