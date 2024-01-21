using Application.Aggregates.Product.Commands;
using AutoMapper;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Core.Domain.Entities;
using MediatR;

namespace Application.Aggregates.Product.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
    {
        private readonly IUow _iUow;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IUow iUow, IMapper mapper)
        {
            _iUow = iUow;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var repository = _iUow.GetRepository<Github.NetCoreWebApp.Core.Domain.Entities.Product>();
            var product = await repository.GetByIdAsync(request.Id);
            await repository.RemoveAsyn(product);
            await _iUow.SaveChanges();

            return Unit.Value;
        }
    }
}
