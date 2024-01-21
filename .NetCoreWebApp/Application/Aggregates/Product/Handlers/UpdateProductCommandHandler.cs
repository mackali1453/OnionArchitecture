using Application.Aggregates.Product.Commands;
using AutoMapper;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Core.Domain.Entities;
using MediatR;

namespace Application.Aggregates.Product.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IUow _iUow;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUow iUow, IMapper mapper)
        {
            _iUow = iUow;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var repository = _iUow.GetRepository<Github.NetCoreWebApp.Core.Domain.Entities.Product>();
            var oldCategory = await repository.GetByIdAsync(request.Id);

            await repository.UpdateAsync(oldCategory, new Github.NetCoreWebApp.Core.Domain.Entities.Product
            {
                CategoryId = request.Id,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            });
            await _iUow.SaveChanges();

            return Unit.Value;
        }
    }
}
