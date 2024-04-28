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
        public UserVehicle Vehicle
        {
            get; set;
        }
        public class UserVehicle
        {
            public string VehicleBrand { get; set; }
            public string VehicleModel { get; set; }
            public string VehicleColor { get; set; }
            public string VehiclePlate { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
