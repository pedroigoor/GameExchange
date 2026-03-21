using GameExchange.Domain.Dtos;
using GameExchange.Domain.Entities;
using GameExchange.Domain.Enum;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Listing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace GameExchange.Infrastructe.DataAccess.Repositories
{
    public class ListingRepository(GameExchangeDbContext dbContext) : IListingWriteOnlyRepository , IListingReadOnlyRepository, IListingUpdateOnlyRepository
    {
        private readonly GameExchangeDbContext _dbContext = dbContext;

        public async Task Add(Listing listing) => await _dbContext.Listings.AddAsync(listing);

        async Task<Listing?> IListingReadOnlyRepository.GetById(long id) => await _dbContext.Listings.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        async Task<Listing?> IListingUpdateOnlyRepository.GetById( long id) => await _dbContext.Listings.FirstOrDefaultAsync(c => c.Id == id);


        public async Task<PagedResult<Listing>> GetPaged(int page, int pageSize, FilterListingDTO filterListing)
        {

            var query = _dbContext.Listings.AsNoTracking();

            var excludedStatus = new List<ListingStatus>
                {
                    ListingStatus.Cancelled,
                    ListingStatus.Draft
                };

            query = query.Where(l => !excludedStatus.Contains(l.Status));

            if (!string.IsNullOrWhiteSpace(filterListing.Title))
                query = query.Where(l => l.Title.Contains(filterListing.Title));

            if (filterListing.PriceMin.HasValue && filterListing.PriceMin.Value >0)
                query = query.Where(l => l.Price >= filterListing.PriceMin);

            if (filterListing.PriceMax.HasValue && filterListing.PriceMax.Value > 0)
                query = query.Where(l => l.Price <= filterListing.PriceMax.Value);

            var totalItems = await query.CountAsync();

            query = query.OrderBy(l => l.Id);

            var data = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            return new PagedResult<Listing>
            {
                Data = data,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }

        public void Update(Listing listing) => _dbContext.Listings.Update(listing);
    }
}
