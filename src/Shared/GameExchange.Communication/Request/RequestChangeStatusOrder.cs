using GameExchange.Communication.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Communication.Request
{
    public class RequestChangeStatusOrder
    {
        public OrderStatus Status { get; set; }
    }
}
