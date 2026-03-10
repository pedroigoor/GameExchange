using GameExchange.Application.UseCases.User;
using GameExchange.Communication.Request;
using Microsoft.AspNetCore.Mvc;

namespace GameExchange.API.Controllers
{
    public class UserController : GameExchangeBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(RequestRegisterUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
         [FromServices] IRegisterUserUseCase useCase,
         [FromBody] RequestRegisterUserJson request)
        {
            var result = await useCase.Execute(request);


            return Created(string.Empty, result);
        }

    }
}
