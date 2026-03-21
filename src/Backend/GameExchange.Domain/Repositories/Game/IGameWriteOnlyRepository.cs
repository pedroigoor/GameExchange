namespace GameExchange.Domain.Repositories.Game
{
    public interface IGameWriteOnlyRepository
    {

        public Task Add(Entities.Game platform);
    }
}
