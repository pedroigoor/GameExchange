using GameExchange.Domain.Evetns;
using GameExchange.Domain.Services.RebbitMQ;
using GameExchange.Infrastructe.Services.RebbitMQ.Mapper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GameExchange.Infrastructe.Services.RebbitMQ
{
    public class RabbitMqConsumer(RabbitMqConnection connection)
                                  : RabbitMqBase(connection), IEventConsumer
    {

        public async Task StartConsuming(string queue, string exchange, string routingKey, Func<string, Task> onMessage, CancellationToken cancellationToken)
        {
            var (conn, channel) = await CreateChannel();

            await channel.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Direct, durable: true, autoDelete: false, cancellationToken: cancellationToken);

            await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null, cancellationToken: cancellationToken); // ← AQUI! Arguments com DLX

            await channel.QueueBindAsync(queue: queue, exchange: exchange, routingKey: routingKey, cancellationToken: cancellationToken);
          
            await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: cancellationToken);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                try
                {
                    await onMessage(json);
                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                }
                catch
                {
                    // aqui você decide:
                    // retry ou dead-letter
                }
            };


            await channel.BasicConsumeAsync(queue: queue, autoAck: false, consumer: consumer, cancellationToken: cancellationToken);

            // mantém vivo
            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
    }
}
