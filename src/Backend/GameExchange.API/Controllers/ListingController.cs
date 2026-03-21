using GameExchange.API.Attributes;
using GameExchange.Application.UseCases.Category.Register;
using GameExchange.Application.UseCases.Game.List;
using GameExchange.Application.UseCases.Listing.ChangeStatus;
using GameExchange.Application.UseCases.Listing.List;
using GameExchange.Application.UseCases.Listing.Register;
using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GameExchange.API.Controllers
{
    [AuthenticatedUser]
    public class ListingController : GameExchangeBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseListingJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save([FromServices] ISaveListingUseCase useCase,
                                        [FromBody] RequestListing request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
        [HttpPut]
        [Route("{id}/status")]
        [ProducesResponseType(typeof(ResponseListingJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeStatus([FromServices] IChangeStatusListingUseCase useCase,
                                                      [FromRoute] long id,
                                                      [FromBody] ChangeStatusRequest request)
        {
            var response = await useCase.Execute(id,request);
            return Ok(response);
        }

        [HttpPost("filter")]
        [ProducesResponseType(typeof(PagedResult<ResponseListingJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> List([FromServices] IFilterListingUseCase useCase,
                                              [FromBody] RequestFilterListing request,
                                              [FromQuery] int page = 1,
                                              [FromQuery] int pageSize = 10)
        {
            var response = await useCase.Execute(page, pageSize,request);
            return Ok(response);
        }
    }
}
