using Application.Aggregates.Product.Commands;
using Application.Aggregates.Product.Queries;
using Github.NetCoreWebApp.Core.Applications.Interfaces;
using Github.NetCoreWebApp.Presentation.Middleware.Logging;
using Github.NetCoreWebApp.Presentation.Middleware.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Github.NetCoreWebApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LoggingAttribute))]

    public class ProductsController : ControllerBase
    {
        private readonly IServiceLogger<ProductsController> _logger;

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator, IServiceLogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await this._mediator.Send(new GetAllProductsQueryRequest());

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await this._mediator.Send(new GetProductQueryRequest(id));

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this._mediator.Send(new DeleteProductCommandRequest(id));

            return Ok(result);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<IActionResult> Create(CreateProductCommandRequest model)
        {
            var result = await this._mediator.Send(model);

            return Ok(result);
        }
        [HttpPut]
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        public async Task<IActionResult> Update(UpdateProductCommandRequest model)
        {
            var result = await this._mediator.Send(model);

            return Ok(result);
        }
    }
}
