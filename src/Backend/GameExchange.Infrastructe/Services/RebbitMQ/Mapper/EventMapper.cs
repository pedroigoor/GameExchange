using GameExchange.Domain.Evetns;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Infrastructe.Services.RebbitMQ.Mapper
{
    public static class EventMapper
    {
        private static readonly Dictionary<string, Type> _map = new()
    {
        { "order.created", typeof(OrderCreatedEvent) },
        { "payment.approved", typeof(PaymentApprovedEvent) }
    };

        public static Type? GetType(string routingKey)
        {
            return _map.TryGetValue(routingKey, out var type)
                ? type
                : null;
        }
    }
}
