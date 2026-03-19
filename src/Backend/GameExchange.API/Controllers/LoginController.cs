using GameExchange.Application.UseCases.Login.LoginInterno;
using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace GameExchange.API.Controllers
{
    public class LoginController : GameExchangeBaseController
    {

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase,
                                              [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}
