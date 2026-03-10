using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GameExchange.Excptions.ExceptionBase
{
    public class ErrorOnValidationException(IList<string> errorMessages) : GameExchangeException(string.Empty)
    {
        private readonly IList<string> _errorMessages = errorMessages;

        public override IList<string> GetErrorMessages() => _errorMessages;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
