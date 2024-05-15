using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Common.Interfaces;
using Domain.Entities.Aggregates;
using MediatR;
using System.Linq.Expressions;

namespace Application.CQRS.Handlers.User
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommandRequest, UserResponseDto>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUtility _utility;

        public UserCreateCommandHandler(IWebApiIuow unitOfWork, IMapper mapper, IUtility utility)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _utility = utility;
        }

        public async Task<UserResponseDto> Handle(UserCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
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
    }
}
