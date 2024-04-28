using Application.Dto;
using MediatR;

namespace Application.CQRS.Commands
{
    public class UserUpdateCommandRequest : IRequest<UserResponseDto>
    {
        public int Id { get; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string MobilePhoneNumber { get; private set; }
        public long Tckn { get; private set; }
        public string Gender { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public List<int> Roles { get; set; }
    }
}
