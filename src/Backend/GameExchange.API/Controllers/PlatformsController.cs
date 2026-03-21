using GameExchange.API.Attributes;
using GameExchange.Application.UseCases.Login.LoginInterno;
using GameExchange.Application.UseCases.Platform.List;
using GameExchange.Application.UseCases.Platform.Register;
using GameExchange.Application.UseCases.Platform.Update;
using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using Microsoft.AspNetCore.Mvc;


namespace GameExchange.API.Controllers
{
    [AuthenticatedUser]
    public class PlatformsController : GameExchangeBaseController
    {

        [HttpPost]
        [ProducesResponseType(typeof(ResponsePlatformJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save([FromServices] ISavePlatformUseCase useCase,
                                             [FromBody] RequestPlatform request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponsePlatformJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromServices] IUpdatePlatformUseCase useCase,
                                              [FromRoute] long id,
                                             [FromBody] RequestPlatform request)
        {
            var response = await useCase.Execute(id,request);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ResponsePlatformJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> List([FromServices] IListPlatformUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }
    }

}
