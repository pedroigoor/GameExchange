using GameExchange.Communication.ConstRabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Domain.Evetns
{
    public record PaymentApprovedEvent(
    long OrderId,
    long ListingId
 ) : IEvent
    {
        public string Exchange => ConstRabbitMQ.PAYMENT_EXCHANGE;
        public string RoutingKey => ConstRabbitMQ.PAYMENT_APROVE_ROUTING_KEY;
        public string Queue => ConstRabbitMQ.PAYMENT_APROVE_QUEUE;
        public string EventId { get; } = Guid.NewGuid().ToString();
    }
}
