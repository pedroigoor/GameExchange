using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GameExchange.Excptions.ExceptionBase
{
    public class InvalidLoginException : GameExchangeException
    {
        public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
