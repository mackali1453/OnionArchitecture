using Application.Aggregates.Category.Queries;
using AutoMapper;
using Github.NetCoreWebApp.Core.Applications.Dto;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using MediatR;

namespace Application.Aggregates.Category.Handlers
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryRequest, CategoryListDto>
    {
        private readonly IUow _iUow;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IUow iUow, IMapper mapper)
        {
            _iUow = iUow;
            _mapper = mapper;
        }

        public async Task<CategoryListDto> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var repository = _iUow.GetRepository<Github.NetCoreWebApp.Core.Domain.Entities.Category>();
            var products = await repository.GetByIdAsync(request.Id);

            return _mapper.Map<CategoryListDto>(products);
        }
    }
}
