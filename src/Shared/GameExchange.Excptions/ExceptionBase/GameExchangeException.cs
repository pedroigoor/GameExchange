using System.Net;

namespace GameExchange.Excptions.ExceptionBase
{
    public abstract class GameExchangeException : SystemException
    {
        public GameExchangeException(string message) : base(message)
        {
        }
        public abstract IList<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }

}
