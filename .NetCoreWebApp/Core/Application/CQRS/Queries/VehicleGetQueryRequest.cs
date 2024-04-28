using Application.Dto;
using MediatR;

namespace Application.CQRS.Queries
{
    public class VehicleGetQueryRequest:IRequest<VehicleResponseDto>
    {
        public int Id { get; set; }
    }
}
