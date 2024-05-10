using Application.CQRS.Commands;
using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Aggregates;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Application.CQRS.Handlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommandRequest, ProductResponseDto>
    {
        private readonly IWebApiIuow _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductUpdateCommandRequest> _validator;
        private readonly int _delayTime;
        public ProductUpdateCommandHandler(IWebApiIuow unitOfWork, IMapper mapper, IValidator<ProductUpdateCommandRequest> validator, IOptions<AppSettings> config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _delayTime = config.Value.DelayTime;
        }

        public async Task<ProductResponseDto> Handle(ProductUpdateCommandRequest updateProductRequest, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(updateProductRequest, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return new ProductResponseDto(false, string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage)), null);
                }
                var repository = _unitOfWork.GetRepository<Product>();
                Expression<Func<Product, bool>> condition = product => product.Id == updateProductRequest.ProductID;
                var eagerExpression = new Expression<Func<Product, object>>[] { e => e.PriceChange };
                var product = await repository.GetByFilterEager(condition, eagerExpression);

                if (product != null && product.Count > 0)
                {
                    product.FirstOrDefault().AddPrice(new Domain.Entities.Aggregates.PriceChange(updateProductRequest.Price, DateTime.Now));
                    await repository.UpdateAsync(product);

                    await Task.Delay(TimeSpan.FromSeconds(_delayTime));

                    await _unitOfWork.SaveChangesAsync();

                    return new ProductResponseDto(true, "", _mapper.Map<List<ProductData>>(product));
                }
                else
                    return new ProductResponseDto(false, "Ürün bulunamadı", null);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update product.", ex);
            }
        }
    }
}
