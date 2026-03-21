using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Listing.List
{
    public interface IFilterListingUseCase
    {
        Task<PagedResult<ResponseListingJson>> Execute(int page, int pageSize, RequestFilterListing request);
    }
}
