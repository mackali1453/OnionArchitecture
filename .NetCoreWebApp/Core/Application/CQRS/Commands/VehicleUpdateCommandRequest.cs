using Application.Dto;
using MediatR;

namespace Application.CQRS.Commands
{
    public class VehicleUpdateCommandRequest : IRequest<VehicleResponseDto>
    {
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleBrand { get; set; }
        public bool IsActive { get; set; }
    }
}
