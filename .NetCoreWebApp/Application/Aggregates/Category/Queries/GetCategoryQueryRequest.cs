using Github.NetCoreWebApp.Core.Applications.Dto;
using MediatR;

namespace Application.Aggregates.Category.Queries
{
    public class GetCategoryQueryRequest : IRequest<CategoryListDto>
    {
        public int Id { get; set; }

        public GetCategoryQueryRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
