using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Domain.Evetns
{
    public interface IEvent
    {
        string Exchange { get; }
        string RoutingKey { get; }
        string Queue { get; }
        string EventId { get; }
    }
}
