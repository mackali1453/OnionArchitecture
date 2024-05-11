using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Aggregates;
using FluentValidation;
using MediatR;
using System.Linq.Expressions;

namespace Application.CQRS.Handlers
{
    public class ProductGetQueryHandler : IRequestHandler<ProductGetQueryRequest, ProductResponseDto>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductGetQueryRequest> _validator;

        public ProductGetQueryHandler(IWebApiIuow unitOfWork, IMapper mapper, IValidator<ProductGetQueryRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ProductResponseDto> Handle(ProductGetQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return new ProductResponseDto(false, string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage)), null);
                }

                var repository = _unitOfWork.GetRepository<Product>();

                Expression<Func<Product, bool>> condition = product =>
                    (request.Price > 0 ? product.PriceChange.OrderByDescending(pc => pc.ChangeDate).FirstOrDefault().Price == request.Price : true) &&
                    (request.Stock > 0 ? product.Stock == request.Stock : true);


                var eagerExpression = new Expression<Func<Product, object>>[] { e => e.PriceChange };

                var products = await repository.GetByFilterEager(condition, eagerExpression);

                if (products == null || !products.Any())
                {
                    return new ProductResponseDto(true, "Product not found!", null);
                }

                return new ProductResponseDto(true, "", _mapper.Map<List<ProductData>>(products));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get products.", ex);
            }
        }
    }
}
