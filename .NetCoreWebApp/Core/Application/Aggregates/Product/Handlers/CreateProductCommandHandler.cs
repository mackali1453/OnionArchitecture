using Application.Aggregates.Product.Commands;
using AutoMapper;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using MediatR;
using Github.NetCoreWebApp.Core.Domain.Entities;

namespace Application.Aggregates.Product.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
    {
        private readonly IUow _iUow;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUow iUow, IMapper mapper)
        {
            _iUow = iUow;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var repository = _iUow.GetRepository<Github.NetCoreWebApp.Core.Domain.Entities.Product>();
                await repository.CreateAsync(new Github.NetCoreWebApp.Core.Domain.Entities.Product
                {
                    CategoryId = request.CategoryId,
                    Name = request.Name,
                    Price = request.Price,
                    Stock = request.Stock
                });
                await _iUow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Unit.Value;
        }
    }
}
