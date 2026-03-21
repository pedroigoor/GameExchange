namespace GameExchange.Domain.Repositories.Category
{
    public interface ICategoryUpdateOnlyRepository
    {

        Task<Entities.Category?> GetById( long id);
        void Update(Entities.Category category);
    }
}
