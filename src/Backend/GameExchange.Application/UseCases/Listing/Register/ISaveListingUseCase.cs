using GameExchange.Communication.Request;
using GameExchange.Communication.Response;

namespace GameExchange.Application.UseCases.Listing.Register
{
    public interface ISaveListingUseCase
    {
        Task<ResponseListingJson> Execute(RequestListing requestListing);
    }
}
