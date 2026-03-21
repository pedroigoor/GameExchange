
using GameExchange.Communication.Enum;

namespace GameExchange.Communication.Response
{
    public class ResponseListingJson
    {
        public long Id { get; set; }
        public long SellerId { get; set; }
        public long GameId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ListingStatus Status { get; set; }
    }
}
