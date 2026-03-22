using GameExchange.Domain.Entities;
using GameExchange.Domain.Repositories.Order;

namespace GameExchange.Infrastructe.DataAccess.Repositories
{
    public class OrderRepositoy(GameExchangeDbContext dbContext) : IOrderWriteOnlyRepository
    {
        private readonly GameExchangeDbContext _dbContext = dbContext;
        public async Task Add(Order order) => await _dbContext.AddAsync(order);
    }
}
