namespace GameExchange.Domain.Repositories.Category
{
    public interface ICategoryReadOnlyRepository
    {
        Task<Entities.Category?> GetById(long id);
        Task<List<Entities.Category>> GetAll();
    }
}
