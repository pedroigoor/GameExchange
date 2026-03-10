using GameExchange.Domain.Entities;
using GameExchange.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace GameExchange.Infrastructe.DataAccess.Repositories
{
    public class UserRepository(GameExchangeDbContext context) : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly GameExchangeDbContext _context = context;

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Status && u.Email.Equals(email));
        }
    }
}
