using Github.NetCoreWebApp.Core.Applications.Dto;
using MediatR;

namespace Application.Aggregates.Category.Queries
{
    public class GetAllCategoriesQueryRequest : IRequest<List<CategoryListDto>>
    {
    }
}
