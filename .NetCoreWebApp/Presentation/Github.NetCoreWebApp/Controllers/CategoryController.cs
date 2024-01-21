using Application.Aggregates.Category.Commands;
using Application.Aggregates.Category.Queries;
using Github.NetCoreWebApp.Presentation.Middleware.Logging;
using Github.NetCoreWebApp.Presentation.Middleware.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Github.NetCoreWebApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LoggingAttribute))]

    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await this._mediator.Send(new GetAllCategoriesQueryRequest());

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await this._mediator.Send(new GetCategoryQueryRequest(id));

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this._mediator.Send(new DeleteCategoryCommandRequest(id));

            return Ok(result);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommandRequest createCategoryCommandRequest)
        {
            var result = await this._mediator.Send(createCategoryCommandRequest);

            return Ok(result);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryCommandRequest updateCategoryCommandRequest)
        {
            var result = await this._mediator.Send(updateCategoryCommandRequest);

            return Ok(result);
        }
    }
}
