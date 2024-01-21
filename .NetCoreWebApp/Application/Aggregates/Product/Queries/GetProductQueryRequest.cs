using Github.NetCoreWebApp.Core.Applications.Dto;
using MediatR;

namespace Application.Aggregates.Product.Queries
{
    public class GetProductQueryRequest : IRequest<ProductListDto>
    {
        public int Id { get; set; }

        public GetProductQueryRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
