using GameExchange.Domain.Evetns;

namespace GameExchange.Domain.Services.RebbitMQ
{
    public interface IEventConsumer
    {
        Task StartConsuming(
            string queue,
            string exchange,
            string routingKey,
            Func<string, Task> onMessage,
            CancellationToken cancellationToken);
    }
}
