using GameExchange.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Infrastructe.DataAccess.Repositories
{
    public class UnitOfWork(GameExchangeDbContext context) : IUnitOfWork
    {
        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }
    }
}
