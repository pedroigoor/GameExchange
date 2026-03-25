using GameExchange.Domain.Evetns;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Domain.Services.RebbitMQ
{
    public interface IEventPublisher
    {
        Task Publish<T>(T @event) where T : IEvent;
    }
}
