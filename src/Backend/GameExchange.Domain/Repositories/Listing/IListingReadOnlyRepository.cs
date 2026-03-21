using GameExchange.Domain.Dtos;

namespace GameExchange.Domain.Repositories.Listing
{
    public interface IListingReadOnlyRepository
    {
        Task<PagedResult<Domain.Entities.Listing>> GetPaged(int page, int pageSize, FilterListingDTO filterListing);
        Task<Domain.Entities.Listing?> GetById(long id);
    }
}
