using GameExchange.API.Attributes;
using GameExchange.Application.UseCases.Game.Register;
using GameExchange.Application.UseCases.Order.Register;
using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace GameExchange.API.Controllers
{
    [AuthenticatedUser]
    public class OrderController : GameExchangeBaseController
    {
        [HttpPost]
        [Route("account/{id}")]
        [ProducesResponseType(typeof(ResponseOrderJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save([FromServices] IRegisterNewOrderUseCase useCase,
                                              [FromRoute] long id)
        {
            var response = await useCase.Execute(id);
            return Created(string.Empty, response);
        }
    }
}
