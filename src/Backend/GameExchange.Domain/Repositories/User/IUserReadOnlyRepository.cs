namespace GameExchange.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<Entities.User?> GetByEmail(string email);
    }
}
