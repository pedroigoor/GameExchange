using GameExchange.Domain.Entities;
using GameExchange.Domain.Repositories.User;

namespace GameExchange.Infrastructe.DataAccess.Repositories
{
    public class UserRepository(GameExchangeDbContext context) : IUserWriteOnlyRepository
    {
        private readonly GameExchangeDbContext _context = context;

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}
