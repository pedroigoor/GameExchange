using GameExchange.API.Attributes;
using GameExchange.Application.UseCases.Game.List;
using GameExchange.Application.UseCases.Game.Register;
using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace GameExchange.API.Controllers
{
    [AuthenticatedUser]
    public class GameController : GameExchangeBaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<ResponseGameJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> List([FromServices] IListGameUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseGameJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save([FromServices] ISaveGameUseCase useCase,
                                           [FromBody] RequestGame request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }

        //[HttpPut]
        //[Route("{id}")]
        //[ProducesResponseType(typeof(ResponseGameJson), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Update([FromServices] IUpdateGameUseCase useCase,
        //                                      [FromRoute] long id,
        //                                     [FromBody] RequestGame request)
        //{
        //    var response = await useCase.Execute(id, request);
        //    return Ok(response);
        //}
    }
}
