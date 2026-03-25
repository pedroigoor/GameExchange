using GameExchange.Domain.Entities;
using GameExchange.Domain.Repositories.Order;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GameExchange.Infrastructe.DataAccess.Repositories
{
    public class OrderRepositoy(GameExchangeDbContext dbContext) : IOrderWriteOnlyRepository, IOrderUpdateOnlyRepository
    {
        private readonly GameExchangeDbContext _dbContext = dbContext;
        public async Task Add(Order order) => await _dbContext.Orders.AddAsync(order);

        async Task<Order?> IOrderUpdateOnlyRepository.GetById(long id) =>  await _dbContext.Orders.FirstOrDefaultAsync(c => c.Id == id);

        public void Update(Order order) => _dbContext.Orders.Update(order);
    }
}
