using GameExchange.Application.UseCases.Listing.ChangeStatus;
using GameExchange.Application.UseCases.Order.UpdateStatus;
using GameExchange.Communication.ConstRabbitMQ;
using GameExchange.Communication.Enum;
using GameExchange.Communication.Request;
using GameExchange.Domain.Evetns;
using GameExchange.Domain.Services.RebbitMQ;
using System.Text.Json;

namespace GameExchange.API.BackgroundServices
{
    public class PaymentRejectedConsumer(IServiceProvider services,
                                      IEventConsumer eventConsumer) : BackgroundService
    {
        private readonly IServiceProvider _services = services;
        private readonly IEventConsumer _eventConsumer = eventConsumer;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _eventConsumer.StartConsuming(
                queue: ConstRabbitMQ.PAYMENT_REJECTED_QUEUE,
                exchange: ConstRabbitMQ.PAYMENT_EXCHANGE,
                routingKey: ConstRabbitMQ.PAYMENT_REJECTED_ROUTING_KEY,
                async (json) =>
                {
                    var @event = JsonSerializer.Deserialize<PaymentRequestedEvent>(json);

                    if (@event != null)

                    {
                        using var scope = _services.CreateScope();

                        var changeListing = scope.ServiceProvider.GetRequiredService<IChangeStatusListingUseCase>();
                        var changeOrder = scope.ServiceProvider.GetRequiredService<IUpdateStatusOrderUseCase>();


                        await changeListing.Execute(@event.ListingId, new RequestChangeStatusListing
                        {
                            Status = ListingStatus.Active
                        });

                        await changeOrder.Execute(@event.OrderId, new RequestChangeStatusOrder
                        {
                            Status = OrderStatus.Cancelled
                        });
                    }


                },
                stoppingToken);
        }
     }
}
