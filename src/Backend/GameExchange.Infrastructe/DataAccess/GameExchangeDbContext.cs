using GameExchange.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameExchange.Infrastructe.DataAccess
{
    public class GameExchangeDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameExchangeDbContext).Assembly);
        }
    }
}
