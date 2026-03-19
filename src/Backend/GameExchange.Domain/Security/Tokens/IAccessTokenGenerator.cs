namespace GameExchange.Domain.Security.Tokens
{
    public interface IAccessTokenGenerator
    {
        public string GenerateToken(Guid userIntentifier);
    }
}
