using GameExchange.API.Attributes;
using GameExchange.Application.UseCases.Category.List;
using GameExchange.Application.UseCases.Category.Register;
using GameExchange.Application.UseCases.Category.Update;
using GameExchange.Application.UseCases.Platform.Register;
using GameExchange.Application.UseCases.Platform.Update;
using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using Microsoft.AspNetCore.Mvc;


namespace GameExchange.API.Controllers
{
    [AuthenticatedUser]
    public class CategoryController : GameExchangeBaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<ResponseCategoryJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> List([FromServices] IListCategoryUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseCategoryJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save([FromServices] ISaveCategoryUseCase useCase,
                                           [FromBody] RequestCategory request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseCategoryJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromServices] IUpdateCategoryUseCase useCase,
                                              [FromRoute] long id,
                                             [FromBody] RequestCategory request)
        {
            var response = await useCase.Execute(id, request);
            return Ok(response);
        }
    }
}
