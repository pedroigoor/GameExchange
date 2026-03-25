using GameExchange.Domain.Evetns;
using GameExchange.Domain.Services.RebbitMQ;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GameExchange.Infrastructe.Services.RebbitMQ
{
    public class RabbitMqPublisher( RabbitMqConnection connection)
                                  : RabbitMqBase(connection), IEventPublisher
    {
        public async Task Publish<T>(T @event) where T : IEvent
        {
            var (connection, channel) = await CreateChannel();
            await using (connection)
            await using (channel)
            {
                await channel.ExchangeDeclareAsync(
                       exchange: @event.Exchange,
                       type: ExchangeType.Direct,
                       durable: true,
                       autoDelete: false);

                await channel.QueueDeclareAsync(
                    queue: @event.Queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null); // ← AQUI! Arguments com DLX




                await channel.QueueBindAsync(
                    queue: @event.Queue,
                    exchange: @event.Exchange,
                    routingKey: @event.RoutingKey
                );


                var properties = new BasicProperties
                {
                    Persistent = true,
                    ContentType = "application/json",
                    ContentEncoding = "utf-8",
                    MessageId = @event.EventId,
                    Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                };


                var message = JsonSerializer.Serialize(@event);
                var body = Encoding.UTF8.GetBytes(message);


                await channel.BasicPublishAsync(
                    exchange: @event.Exchange,
                    routingKey: @event.RoutingKey,
                    mandatory: false,
                    basicProperties: properties,
                    body: body);
            }
           

        }
    }

}