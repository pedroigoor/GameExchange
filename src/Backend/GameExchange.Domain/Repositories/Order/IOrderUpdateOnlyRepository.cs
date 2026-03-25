using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Domain.Repositories.Order
{
    public interface IOrderUpdateOnlyRepository
    {
        Task<Entities.Order?> GetById(long id);

        void Update(Entities.Order order);
    }
}
