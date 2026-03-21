namespace GameExchange.Domain.Repositories.Category
{
    public interface ICategoryWriteOnlyRepository
    {

        public Task Add(Entities.Category category);
    }
}
