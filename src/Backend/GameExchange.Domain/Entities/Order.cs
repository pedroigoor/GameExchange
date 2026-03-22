using GameExchange.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Domain.Entities
{
    public class Order(long buyerId, long listingId, decimal price) : EntityBase
    {
        public long BuyerId { get; set; } = buyerId;
        public long ListingId { get; set; } = listingId;

        public decimal Price { get; set; } = price;
        public OrderStatus Status { get; set; } = OrderStatus.PendingPayment;
    }
}
