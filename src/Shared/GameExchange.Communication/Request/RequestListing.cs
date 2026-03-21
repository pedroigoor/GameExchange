using GameExchange.Communication.Enum;

namespace GameExchange.Communication.Request
{
    public class RequestListing
    {
        public long GameId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
