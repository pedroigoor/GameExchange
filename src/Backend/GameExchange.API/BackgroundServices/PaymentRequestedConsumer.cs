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
    public class PaymentRequestedConsumer(
                                      IEventConsumer eventConsumer,
                                      IEventPublisher publisher) : BackgroundService
    {
       
        private readonly IEventConsumer _eventConsumer = eventConsumer;
        private readonly IEventPublisher _publisher = publisher;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _eventConsumer.StartConsuming(
                queue: ConstRabbitMQ.PAYMENT_REQUESTED_QUEUE, 
                exchange: ConstRabbitMQ.PAYMENT_EXCHANGE,
                routingKey: ConstRabbitMQ.PAYMENT_REQUESTED_ROUTING_KEY,
                async (json) =>
                {                    

                    var @event = JsonSerializer.Deserialize<PaymentRequestedEvent>(json);

                    if (@event == null) return;

                    Console.WriteLine($"Processando pagamento do pedido {@event.OrderId}");

                    
                    await Task.Delay(2000); // simula tempo de gateway

                    var random = new Random();
                    var approved = random.Next(0, 100) > 20; // 80% aprovado

                    if (approved)
                    {
                     

                        await _publisher.Publish(new PaymentApprovedEvent(
                            @event.OrderId,
                            @event.ListingId
                        ));
                    }
                    else
                    {
                      
                        await _publisher.Publish(new PaymentRejectedEvent(
                            @event.OrderId,
                            @event.ListingId
                        ));
                    }

                },
                stoppingToken);
        }
    }
}
