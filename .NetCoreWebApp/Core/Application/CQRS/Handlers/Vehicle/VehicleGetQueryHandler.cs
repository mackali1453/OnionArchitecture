using Application.CQRS.Queries;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;
using MediatR;
using System.Linq.Expressions;

namespace Application.CQRS.Handlers.Vehicle
{
    public class VehicleGetQueryHandler : IRequestHandler<VehicleGetQueryRequest, VehicleResponseDto>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleGetQueryHandler(IWebApiIuow unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<VehicleResponseDto> Handle(VehicleGetQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicleRepository = _unitOfWork.GetRepository<AppVehicle>();
                Expression<Func<AppVehicle, bool>> condition = vehicle => vehicle.Id == request.Id;
                var eagerExpression = new Expression<Func<AppVehicle, object>>[] { e => e.AppUser };

                var vehicle = await vehicleRepository.GetByFilterEager(condition, eagerExpression);

                if (vehicle == null)
                {
                    return new VehicleResponseDto(true, "Vehicle not found!", null);
                }

                return new VehicleResponseDto(true, "", _mapper.Map<VehicleData>(vehicle));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get vehicle by id.", ex);
            }
        }
    }
}
