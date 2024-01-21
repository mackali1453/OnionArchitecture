using Application.Aggregates.Category.Queries;
using AutoMapper;
using Github.NetCoreWebApp.Core.Applications.Dto;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using MediatR;

namespace Application.Aggregates.Category.Handlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, List<CategoryListDto>>
    {
        private readonly IUow _iUow;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IUow ıUow, IMapper mapper)
        {
            _iUow = ıUow;
            _mapper = mapper;
        }

        public async Task<List<CategoryListDto>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var repository = _iUow.GetRepository<Github.NetCoreWebApp.Core.Domain.Entities.Category>();
            var products = await repository.GetAllAsync();

            return _mapper.Map<List<CategoryListDto>>(products);
        }
    }
}
