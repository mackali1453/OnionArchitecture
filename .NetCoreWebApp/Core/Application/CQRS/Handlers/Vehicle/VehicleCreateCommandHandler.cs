using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;
using MediatR;

namespace Application.CQRS.Handlers.Vehicle
{
    public class VehicleCreateCommandHandler : IRequestHandler<VehicleCreateCommandRequest, VehicleResponseDto>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleCreateCommandHandler(IWebApiIuow unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<VehicleResponseDto> Handle(VehicleCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicleRepository = _unitOfWork.GetRepository<AppVehicle>();
                var appUserRepository = _unitOfWork.GetRepository<AppUser>();

                var user = await appUserRepository.GetByIdAsync(request.UserId);

                if (user == null)
                {
                    return new VehicleResponseDto(true, "User not found!", null);
                }

                var newVehicle = _mapper.Map<AppVehicle>(request);
                newVehicle.SetRelationWithUser(user);

                await vehicleRepository.CreateAsync(newVehicle);
                await _unitOfWork.SaveChangesAsync();

                return new VehicleResponseDto(true, "", _mapper.Map<VehicleData>(newVehicle));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create vehicle.", ex);
            }
        }
    }
}
