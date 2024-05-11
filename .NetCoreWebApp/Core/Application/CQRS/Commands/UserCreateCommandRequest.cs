using Application.Dto;
using MediatR;

namespace Application.CQRS.Commands
{
    public class UserCreateCommandRequest : IRequest<UserResponseDto>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MobilePhoneNumber { get; set; }
        public long Tckn { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}