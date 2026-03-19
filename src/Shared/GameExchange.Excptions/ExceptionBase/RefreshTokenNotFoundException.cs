using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GameExchange.Excptions.ExceptionBase
{
    public class RefreshTokenNotFoundException : GameExchangeException
    {
        public RefreshTokenNotFoundException() : base(ResourceMessagesException.EXPIRED_SESSION)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
