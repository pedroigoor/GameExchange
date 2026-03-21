namespace GameExchange.Communication.Request
{
    public class RequestFilterListing
    {
        public string? Title { get; set; }
        public decimal? PriceMax { get; set; }
        public decimal? PriceMin { get; set; }
    }
}
