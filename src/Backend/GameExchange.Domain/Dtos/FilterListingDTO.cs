using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Domain.Dtos
{
    public class FilterListingDTO
    {
        public string? Title { get; set; }
        public decimal? PriceMax { get; set; }
        public decimal? PriceMin { get; set; }
    }
}
