using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Communication.Request
{
    public class RequestNewTokenJson
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
