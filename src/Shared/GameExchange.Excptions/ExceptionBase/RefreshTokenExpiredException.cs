using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GameExchange.Excptions.ExceptionBase
{
    public class RefreshTokenExpiredException : GameExchangeException
    {
        public RefreshTokenExpiredException() : base(ResourceMessagesException.INVALID_SESSION)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
    }
}
