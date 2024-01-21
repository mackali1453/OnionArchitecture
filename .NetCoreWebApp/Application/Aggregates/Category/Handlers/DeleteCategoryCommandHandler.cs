using Application.Aggregates.Category.Commands;
using AutoMapper;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using MediatR;

namespace Application.Aggregates.Category.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest>
    {
        private readonly IUow _iUow;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(IUow iUow, IMapper mapper)
        {
            _iUow = iUow;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var repository = _iUow.GetRepository<Github.NetCoreWebApp.Core.Domain.Entities.Category>();
            var product = await repository.GetByIdAsync(request.Id);
            await repository.RemoveAsyn(product);
            await _iUow.SaveChanges();

            return Unit.Value;
        }
    }
}
