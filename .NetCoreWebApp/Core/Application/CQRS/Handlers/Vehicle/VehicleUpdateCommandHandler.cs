using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;
using MediatR;
using System.Linq.Expressions;

namespace Application.CQRS.Handlers.Vehicle
{
    public class VehicleUpdateCommandHandler : IRequestHandler<VehicleUpdateCommandRequest, VehicleResponseDto>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleUpdateCommandHandler(IWebApiIuow unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VehicleResponseDto> Handle(VehicleUpdateCommandRequest updateVehicleRequest, CancellationToken cancellationToken)
        {
            try
            {
                var vehicleRepository = _unitOfWork.GetRepository<AppVehicle>();
                var oldVehicle = await vehicleRepository.GetByIdAsync(updateVehicleRequest.VehicleId);

                if (oldVehicle == null)
                {
                    return new VehicleResponseDto(true, "Vehicle not found!", null);
                }

                if (oldVehicle.IsActive == updateVehicleRequest.IsActive)
                {
                    var newVehicle = _mapper.Map<AppVehicle>(updateVehicleRequest);
                    await vehicleRepository.UpdateAsync(newVehicle, oldVehicle);
                    await _unitOfWork.SaveChangesAsync();

                    return new VehicleResponseDto(true, "", _mapper.Map<VehicleData>(newVehicle));
                }

                Expression<Func<AppVehicle, bool>> condition = vehicle => vehicle.AppUser.Id == updateVehicleRequest.UserId && vehicle.IsActive == true;
                var oldActiveVehicles = (await vehicleRepository.GetByFilter(condition));

                if (oldActiveVehicles != null && oldActiveVehicles.Count > 0)
                {
                    await vehicleRepository.UpdateAsync(oldActiveVehicles.Select(x =>
                    {
                        x.UpdateIsActive(false);
                        return x;
                    }));
                }

                var updatedVehicle = _mapper.Map<AppVehicle>(updateVehicleRequest);
                await vehicleRepository.UpdateAsync(updatedVehicle, oldVehicle);
                await _unitOfWork.SaveChangesAsync();

                return new VehicleResponseDto(true, "", _mapper.Map<VehicleData>(updatedVehicle));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update vehicle.", ex);
            }
        }
    }
}
