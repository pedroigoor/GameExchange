using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Domain.Repositories.Order
{
    public interface IOrderWriteOnlyRepository
    {
        public Task Add(Entities.Order order);
    }
}
