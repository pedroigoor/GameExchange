using GameExchange.Communication.ConstRabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Domain.Evetns
{
        public record PaymentRequestedEvent(
        long OrderId,
        long ListingId,
        decimal Amount
    ) : IEvent
        {
            public string Exchange => ConstRabbitMQ.PAYMENT_EXCHANGE;
            public string RoutingKey => ConstRabbitMQ.PAYMENT_REQUESTED_ROUTING_KEY;
            public string Queue => ConstRabbitMQ.PAYMENT_REQUESTED_QUEUE;
            public string EventId { get; } = Guid.NewGuid().ToString();
        }
}
