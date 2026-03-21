namespace GameExchange.Domain.Repositories.Game
{
    public interface IGameUpdateOnlyRepository
    {

        Task<Entities.Game?> GetById( long id);
        void Update(Entities.Game game);
    }
}
