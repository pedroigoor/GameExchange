namespace GameExchange.Domain.Repositories.Platform
{
    public interface IPlatformUpdateOnlyRepository
    {

        Task<Entities.Platform?> GetById( long id);
        void Update(Entities.Platform platform);
    }
}
