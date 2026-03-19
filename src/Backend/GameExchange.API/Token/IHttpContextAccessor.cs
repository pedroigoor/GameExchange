using GameExchange.Domain.Security.Tokens;

namespace GameExchange.API.Token
{
    public class HttpContextTokenValue(IHttpContextAccessor httpContextAccessor) : ITokenProvider
    {

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string Value()
        {
            var token = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
            return token["Bearer ".Length..].Trim();
        }
    }
}
