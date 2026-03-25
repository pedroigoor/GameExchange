using GameExchange.Communication.ConstRabbitMQ;

namespace GameExchange.Domain.Evetns
{
    public record OrderCreatedEvent(
     long OrderId,
     long ListingId,
     long BuyerId,
     decimal Price
 ) : IEvent
    {
        public string Exchange => ConstRabbitMQ.ORDER_EXCHANGE;
        public string RoutingKey => ConstRabbitMQ.ORDER_CREATED_ROUTING_KEY;
        public string Queue => ConstRabbitMQ.ORDER_CREATED_QUEUE;
        public string EventId { get; } = Guid.NewGuid().ToString();
    }
}
