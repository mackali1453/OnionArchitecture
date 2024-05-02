using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;
using System.Linq.Expressions;

namespace Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleService(IWebApiIuow unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<VehicleResponseDto> GetById(int id)
        {
            try
            {
                var vehicleRepository = _unitOfWork.GetRepository<AppVehicle>();
                Expression<Func<AppVehicle, bool>> condition = vehicle => vehicle.Id == id;
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

        public async Task Delete(int id)
        {
            try
            {
                var vehicleRepository = _unitOfWork.GetRepository<AppVehicle>();
                var vehicle = await vehicleRepository.GetByIdAsync(id);

                if (vehicle == null)
                {
                    vehicleRepository.Remove(vehicle);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete vehicle.", ex);
            }
        }

        public async Task<VehicleResponseDto> Update(VehicleUpdateCommandRequest request)
        {
            try
            {
                var vehicleRepository = _unitOfWork.GetRepository<AppVehicle>();
                var oldVehicle = await vehicleRepository.GetByIdAsync(request.VehicleId);

                if (oldVehicle == null)
                {
                    return new VehicleResponseDto(true, "Vehicle not found!", null);
                }

                if (oldVehicle.IsActive == request.IsActive)
                {
                    var newVehicle = _mapper.Map<AppVehicle>(request);
                    await vehicleRepository.UpdateAsync(newVehicle, oldVehicle);
                    await _unitOfWork.SaveChangesAsync();

                    return new VehicleResponseDto(true, "", _mapper.Map<VehicleData>(newVehicle));
                }

                Expression<Func<AppVehicle, bool>> condition = vehicle => vehicle.AppUser.Id == request.UserId && vehicle.IsActive == true;
                var oldActiveVehicles = (await vehicleRepository.GetByFilter(condition));

                if (oldActiveVehicles != null && oldActiveVehicles.Count > 0)
                {
                    await vehicleRepository.UpdateAsync(oldActiveVehicles.Select(x =>
                    {
                        x.UpdateIsActive(false);
                        return x;
                    }));
                }

                var updatedVehicle = _mapper.Map<AppVehicle>(request);
                await vehicleRepository.UpdateAsync(updatedVehicle, oldVehicle);
                await _unitOfWork.SaveChangesAsync();

                return new VehicleResponseDto(true, "", _mapper.Map<VehicleData>(updatedVehicle));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update vehicle.", ex);
            }
        }

        public async Task<VehicleResponseDto> Create(VehicleCreateCommandRequest request)
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

        public Task<VehicleResponseDto> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
