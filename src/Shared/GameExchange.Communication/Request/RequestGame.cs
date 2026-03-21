using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Communication.Request
{
    public class RequestGame
    {
        public string Name { get; set; } = string.Empty;
        public long CategoryId { get; set; }
        public long PlatformId { get; set; }
    }
}
