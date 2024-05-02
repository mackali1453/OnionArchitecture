using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;
using System.Linq.Expressions;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUtility _utility;

        public UserService(IWebApiIuow unitOfWork, IMapper mapper, IUtility utility)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _utility = utility ?? throw new ArgumentNullException(nameof(utility));
        }

        public async Task<UserResponseDto> GetById(int id)
        {
            try
            {
                var userRepo = _unitOfWork.GetRepository<AppUser>();

                Expression<Func<AppUser, bool>> condition = person => person.Id == id;
                var eagerExpression = new Expression<Func<AppUser, object>>[] { e => e.AppVehicle };

                var user = await userRepo.GetByFilterEager(condition, eagerExpression);

                if (user == null)
                {
                    return new UserResponseDto(true, $"User with ID {id} not found.", null);
                }

                var response = _mapper.Map<UserData>(user);

                return new UserResponseDto(true, "", response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve user with ID {id}.", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var userRepo = _unitOfWork.GetRepository<AppUser>();

                var user = await userRepo.GetByIdAsync(id);

                if (user != null)
                {
                    userRepo.Remove(user);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete user with ID {id}.", ex);
            }
        }

        public async Task<UserResponseDto> Update(UserUpdateCommandRequest updateUserRequest)
        {
            try
            {
                if (updateUserRequest == null)
                {
                    throw new ArgumentNullException(nameof(updateUserRequest));
                }

                var newUser = new AppUser(
                   updateUserRequest.Name,
                   updateUserRequest.Surname,
                   updateUserRequest.MobilePhoneNumber,
                   updateUserRequest.UserName,
                   updateUserRequest.Password,
                   updateUserRequest.Tckn,
                   updateUserRequest.Gender,
                   updateUserRequest.Id
               );

                var userRepo = _unitOfWork.GetRepository<AppUser>();

                var user = await userRepo.GetByIdAsync(updateUserRequest.Id);

                if (user == null)
                {
                    return new UserResponseDto(true, $"User with ID {updateUserRequest.Id} not found.", null);
                }

                await userRepo.UpdateAsync(newUser, user);
                await _unitOfWork.SaveChangesAsync();

                return new UserResponseDto(true, "", _mapper.Map<UserData>(newUser));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update user.", ex);
            }
        }

        public async Task<UserResponseDto> Create(UserCreateCommandRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                var userRepo = _unitOfWork.GetRepository<AppUser>();

                Expression<Func<AppUser, bool>> condition = person => person.MobilePhoneNumber == request.MobilePhoneNumber || person.UserName == request.UserName;
                var dbUser = await userRepo.GetByFilter(condition);

                if (dbUser != null && dbUser.Count > 0)
                {
                    return new UserResponseDto(false, "Bu cep telefonu ve username ile kullanıcı mevcuttur!", null);
                }

                var newVehicle = _mapper.Map<AppVehicle>(request.Vehicle);
                var newParkingLot = new ParkingLot(new long(), new long());

                var newUser = new AppUser(
                        request.Name,
                        request.Surname,
                        request.MobilePhoneNumber,
                        request.UserName,
                        _utility.HashPassword(request.Password),
                        request.Tckn,
                        request.Gender
                    );

                newUser.SetRelationWithVehicle(newVehicle);
                newUser.SetRelationWithParkingLot(newParkingLot);

                await userRepo.CreateAsync(newUser);
                await _unitOfWork.SaveChangesAsync();

                return new UserResponseDto(true, "", _mapper.Map<UserData>(newUser));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create user.", ex);
            }
        }

        public Task<UserResponseDto> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
