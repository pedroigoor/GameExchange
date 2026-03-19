using GameExchange.Domain.Entities;

namespace GameExchange.Domain.Services
{
    public interface ILoggedUser
    {
        public Task<User> User();
    }
}
