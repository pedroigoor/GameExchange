using System.Net;

namespace GameExchange.Excptions.ExceptionBase
{
    public class NotFoundException(string message) : GameExchangeException(message)
    {
        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
