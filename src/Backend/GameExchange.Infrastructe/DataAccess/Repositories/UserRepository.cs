using GameExchange.Domain.Entities;
using GameExchange.Domain.Repositories.User;

namespace GameExchange.Infrastructe.DataAccess.Repositories
{
    public class UserRepository : IUserWriteOnlyRepository
    {
        private readonly GameExchangeDbContext _context;
        public UserRepository(GameExchangeDbContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
