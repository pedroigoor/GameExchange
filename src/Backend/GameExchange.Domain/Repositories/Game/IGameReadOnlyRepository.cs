namespace GameExchange.Domain.Repositories.Game
{
    public interface IGameReadOnlyRepository
    {
        Task<Entities.Game?> GetById(long id);
        Task<List<Entities.Game>> GetAll();
    }
}
