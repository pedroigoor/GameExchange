using Microsoft.EntityFrameworkCore;

namespace GameExchange.Infrastructe.DataAccess
{
    public class GameExchangeDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameExchangeDbContext).Assembly);
        }
    }
}
