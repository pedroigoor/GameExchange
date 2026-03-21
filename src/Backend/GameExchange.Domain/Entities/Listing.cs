using GameExchange.Domain.Enum;

namespace GameExchange.Domain.Entities
{
    public class Listing : EntityBase
    {
        public long SellerId { get;  set; }
        public long GameId { get;  set; }
        public string Title { get;  set; } = string.Empty;
        public string Description { get;  set; } = string.Empty;
        public decimal Price { get;  set; }
        public ListingStatus Status { get;  set; }


    }
}
