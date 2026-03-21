namespace GameExchange.Domain.Repositories.Platform
{
    public interface IPlatformWriteOnlyRepository
    {

        public Task Add(Entities.Platform platform);
    }
}
