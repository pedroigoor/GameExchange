using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Domain.Repositories.Listing
{
    public interface IListingUpdateOnlyRepository
    {
        Task<Entities.Listing?> GetById(Entities.User? user, long id);

        void Update(Entities.Listing listing);
    }
}
