using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Handlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommandRequest, ProductResponseDto>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductCreateCommandRequest> _validator;

        public ProductCreateCommandHandler(IWebApiIuow unitOfWork, IMapper mapper, IValidator<ProductCreateCommandRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ProductResponseDto> Handle(ProductCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return new ProductResponseDto(false, string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage)), null);
                }

                var repository = _unitOfWork.GetRepository<Product>();
                var newProduct = new Product(request.ProductName, request.Stock);
                newProduct.AddPrice(new Domain.Entities.Aggregates.PriceChange(request.Price, DateTime.Now));

                await repository.CreateAsync(newProduct);
                await _unitOfWork.SaveChangesAsync();

                return new ProductResponseDto(true, "", _mapper.Map<List<ProductData>>(new List<Product> { newProduct }));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create product.", ex);
            }
        }
    }
}
