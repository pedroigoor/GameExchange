namespace GameExchange.Domain.Repositories.Platform
{
    public interface IPlatformReadOnlyRepository
    {
        Task<Entities.Platform?> GetById(long id);
        Task<List<Entities.Platform>> GetAll();
    }
}
