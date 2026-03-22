using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GameExchange.Excptions.ExceptionBase
{
    public class ErrorOnValidationException : GameExchangeException
    {
        private readonly IList<string> _errorMessages = [];

        public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
        {
            _errorMessages = errorMessages;
        }

        public ErrorOnValidationException(string errorMessage) : base(string.Empty)
        {
            _errorMessages.Add(errorMessage);
        }

        public override IList<string> GetErrorMessages() => _errorMessages;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
