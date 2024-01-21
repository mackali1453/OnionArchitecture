using Application.Aggregates.Product.Queries;
using AutoMapper;
using Github.NetCoreWebApp.Core.Applications.Dto;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Core.Domain.Entities;
using MediatR;

namespace Application.Aggregates.Product.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, List<ProductListDto>>
    {
        private readonly IUow _iUow;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IUow ıUow, IMapper mapper)
        {
            _iUow = ıUow;
            _mapper = mapper;
        }

        public async Task<List<ProductListDto>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var repository = _iUow.GetRepository<Github.NetCoreWebApp.Core.Domain.Entities.Product>();
            var products = await repository.GetAllAsync();

            return _mapper.Map<List<ProductListDto>>(products);
        }
    }
}
