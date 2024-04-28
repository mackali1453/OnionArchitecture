using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;
using Github.NetCoreWebApp.Core.Application.Interfaces;
using System.Linq.Expressions;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IWebApiIuow _iUow;
        private readonly IMapper _mapper;
        private readonly IServiceLogger<UserService> _logger;
        private readonly IUtility _utility;

        public UserService(IWebApiIuow iUow, IMapper mapper, IServiceLogger<UserService> logger, IUtility utility)
        {
            _iUow = iUow;
            _mapper = mapper;
            _logger = logger;
            _utility = utility;
        }

        public async Task<UserResponseDto> GetById(int id)
        {
            var user = new AppUser();

            try
            {
                var userRepo = _iUow.GetRepository<AppUser>();

                Expression<Func<AppUser, bool>> condition = person => person.Id == id;
                var eagerExpression = new Expression<Func<AppUser, object>>[] { e => e.AppVehicle };

                user = await userRepo.GetByFilterEager(condition, eagerExpression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new UserResponseDto(true, "", _mapper.Map<UserData>(user));
        }
        public async Task Delete(int id)
        {
            try
            {
                var userRepo = _iUow.GetRepository<AppUser>();

                var user = await userRepo.GetByIdAsync(id);
                userRepo.Remove(user);
                await _iUow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserResponseDto> Update(UserUpdateCommandRequest updateUserRequest)
        {
            var newUser = new AppUser(
               updateUserRequest.Name,
               updateUserRequest.Surname,
               updateUserRequest.MobilePhoneNumber,
               updateUserRequest.UserName,
               updateUserRequest.Password,
               updateUserRequest.Tckn,
               updateUserRequest.Gender
           );

            try
            {
                var userRepo = _iUow.GetRepository<AppUser>();

                var user = await userRepo.GetByIdAsync(updateUserRequest.Id);

                await userRepo.UpdateAsync(newUser, user);

                await _iUow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new UserResponseDto(true, "", _mapper.Map<UserData>(newUser));
        }
        public async Task<UserResponseDto> Create(UserCreateCommandRequest request)
        {
            var newUser = new AppUser(
                    request.Name,
                    request.Surname,
                    request.MobilePhoneNumber,
                    request.UserName,
                    _utility.HashPassword(request.Password),
                    request.Tckn,
                    request.Gender
                );

            try
            {
                var userRepo = _iUow.GetRepository<AppUser>();
                Expression<Func<AppUser, bool>> condition = person => person.MobilePhoneNumber == request.MobilePhoneNumber;

                var dbUser = await userRepo.GetByFilter(condition);

                if (dbUser != null)
                {
                    return new UserResponseDto(false, "Bu eposta adresi ile kullanıcı mevcuttur!", null);
                }

                var newVehicle = new AppVehicle(request.Vehicle.VehiclePlate, request.Vehicle.VehicleColor, request.Vehicle.VehicleModel, request.Vehicle.VehicleBrand, request.Vehicle.IsActive);
                var newParkingLot = new ParkingLot(new long(), new long());

                newUser.SetRelationWithVehicle(newVehicle);
                newUser.SetRelationWithParkingLot(newParkingLot);
                await userRepo.CreateAsync(newUser);

                await _iUow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new UserResponseDto(true, "", _mapper.Map<UserData>(newUser));
        }
    }
}
