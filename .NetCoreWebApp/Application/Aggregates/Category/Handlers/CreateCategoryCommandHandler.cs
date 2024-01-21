using Application.Aggregates.Category.Commands;
using AutoMapper;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using MediatR;

namespace Application.Aggregates.Category.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest>
    {
        private readonly IUow _iUow;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUow iUow, IMapper mapper)
        {
            _iUow = iUow;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var repository = _iUow.GetRepository<Github.NetCoreWebApp.Core.Domain.Entities.Category>();
            await repository.CreateAsync(new Github.NetCoreWebApp.Core.Domain.Entities.Category
            {
                Definition = request.Definition
            });
            await _iUow.SaveChanges();

            return Unit.Value;
        }
    }
}
