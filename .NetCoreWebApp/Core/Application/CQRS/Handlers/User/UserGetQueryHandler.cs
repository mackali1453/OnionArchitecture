using Application.CQRS.Queries;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Common.Interfaces;
using Domain.Entities.Aggregates;
using MediatR;
using System.Linq.Expressions;

namespace Application.CQRS.Handlers.User
{
    public class UserGetQueryHandler : IRequestHandler<UserGetQueryRequest, UserResponseDto>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;

        public UserGetQueryHandler(IWebApiIuow unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> Handle(UserGetQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userRepo = _unitOfWork.GetRepository<AppUser>();

                Expression<Func<AppUser, bool>> condition = person => person.Id == request.Id;
                var eagerExpression = new Expression<Func<AppUser, object>>[] { e => e.AppVehicle };

                var user = await userRepo.GetByFilterEager(condition, eagerExpression);

                if (user == null)
                {
                    return new UserResponseDto(true, $"User with ID {request.Id} not found.", null);
                }

                var response = _mapper.Map<UserData>(user);

                return new UserResponseDto(true, "", response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve user with ID {request.Id}.", ex);
            }
        }
    }
}
