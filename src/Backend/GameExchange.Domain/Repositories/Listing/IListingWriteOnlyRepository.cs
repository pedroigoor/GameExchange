namespace GameExchange.Domain.Repositories.Listing
{
    public interface IListingWriteOnlyRepository
    {
        public Task Add(Entities.Listing listing);
    }
}
