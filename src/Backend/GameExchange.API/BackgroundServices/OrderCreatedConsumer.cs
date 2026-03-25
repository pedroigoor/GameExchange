using GameExchange.Application.UseCases.Listing.ChangeStatus;
using GameExchange.Communication.ConstRabbitMQ;
using GameExchange.Communication.Enum;
using GameExchange.Communication.Request;
using GameExchange.Domain.Evetns;
using GameExchange.Domain.Services.RebbitMQ;
using System.Text.Json;

namespace GameExchange.API.BackgroundServices
{
    public class OrderCreatedConsumer(IServiceProvider services,
                                      IEventConsumer eventConsumer,
                                      IEventPublisher publisher) : BackgroundService
    {
        private readonly IServiceProvider _services = services;
        private readonly IEventConsumer _eventConsumer = eventConsumer;
        private readonly IEventPublisher _publisher = publisher;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _eventConsumer.StartConsuming(
                queue: ConstRabbitMQ.ORDER_CREATED_QUEUE,
                exchange: ConstRabbitMQ.ORDER_EXCHANGE,
                routingKey: ConstRabbitMQ.ORDER_CREATED_ROUTING_KEY,
                async (json) =>
                {
                    using var scope = _services.CreateScope();

                   

                    var @event = JsonSerializer.Deserialize<OrderCreatedEvent>(json);

                    if (@event != null) {
                        var changeListing = scope.ServiceProvider.GetRequiredService<IChangeStatusListingUseCase>();

                        await changeListing.Execute(@event.ListingId, new RequestChangeStatusListing {
                                                                               Status = ListingStatus.Reserved});

                        await _publisher.Publish(new PaymentRequestedEvent(
                                           @event.OrderId,
                                           @event.ListingId,
                                           @event.Price
                                       ));
                    }


                },
                stoppingToken);
        }
    }
}
