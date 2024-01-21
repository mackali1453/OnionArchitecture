using Github.NetCoreWebApp.Core.Applications.Dto;
using MediatR;

namespace Application.Aggregates.Product.Queries
{
    public class GetAllProductsQueryRequest : IRequest<List<ProductListDto>>
    {
    }
}
