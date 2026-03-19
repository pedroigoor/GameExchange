using GameExchange.Application.UseCases.Token.RefreshToken;
using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace GameExchange.API.Controllers
{
    public class TokenController : GameExchangeBaseController
    {
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(ResponseTokensJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken(
            [FromServices] IUseRefreshTokenUseCase useCase,
            [FromBody] RequestNewTokenJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }
    }
}
