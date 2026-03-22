using GameExchange.Communication.Request;
using GameExchange.Communication.Response;

namespace GameExchange.Application.UseCases.Listing.Update
{
    public interface IUpdateListingUseCase
    {
        public Task<ResponseListingJson> Execute(long id, RequestListing request);
    }
}
