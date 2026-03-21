using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Listing;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Listing.List
{
    public class FilterListingUseCase(IListingReadOnlyRepository listingRepository) : IFilterListingUseCase
    {
        private readonly IListingReadOnlyRepository _listingRepository= listingRepository;

        public async Task<PagedResult<ResponseListingJson>> Execute(int page, int pageSize, RequestFilterListing request)
        {

            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var filters = new Domain.Dtos.FilterListingDTO
            {
                
                PriceMax= request.PriceMax,
                PriceMin= request.PriceMin,
                Title = request.Title
            };
            var result = await _listingRepository.GetPaged(page, pageSize, filters);
            
            return result.Adapt<PagedResult<ResponseListingJson>>();
        }
    }
}
