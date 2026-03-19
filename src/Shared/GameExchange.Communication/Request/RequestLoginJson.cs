using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Communication.Request
{
    public class RequestLoginJson
    {
        public string Email { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
    }
}
