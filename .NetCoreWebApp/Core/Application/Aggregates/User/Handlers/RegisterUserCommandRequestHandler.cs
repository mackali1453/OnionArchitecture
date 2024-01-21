using Application.Aggregates.User.Commands;
using Application.Enums;
using AutoMapper;
using Domain.Entities;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Core.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.Aggregates.User.Handlers
{
    public class RegisterUserCommandRequestHandler : IRequestHandler<RegisterUserCommandRequest>
    {
        private readonly IUow _iUow;
        private readonly IMapper _mapper;

        public RegisterUserCommandRequestHandler(IUow iUow, IMapper mapper)
        {
            _iUow = iUow;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            var userRepository = _iUow.GetRepository<AppUser>();
            var roleRepository = _iUow.GetRepository<AppUserRole>();

            await userRepository.CreateAsync(new AppUser
            {
                UserName = request.Username,
                Password = request.Password,
            });

            Expression<Func<AppUser, bool>> condition = person => person.UserName == request.Username && person.Password == request.Password;

            var createdPerson = await userRepository.GetByFilter(condition);
            await roleRepository.CreateAsync(new AppUserRole { RoleId = (int)RoleTypes.Member, UserId = createdPerson.UserId });

            await _iUow.SaveChanges();

            return Unit.Value;
        }
    }
}
