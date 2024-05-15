using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Common.Interfaces;
using Domain.Entities.Aggregates;
using MediatR;

namespace Application.CQRS.Handlers.User
{
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommandRequest, UserResponseDto>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;

        public UserUpdateCommandHandler(IWebApiIuow unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserResponseDto> Handle(UserUpdateCommandRequest updateUserRequest, CancellationToken cancellationToken)
        {
            try
            {
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
    }
}
