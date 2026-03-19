using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories.User;
using GameExchange.Domain.Security.Tokens;
using GameExchange.Excptions;
using GameExchange.Excptions.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace GameExchange.API.Filters
{
    public class AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator,
                                    IUserReadOnlyRepository repository) : IAsyncAuthorizationFilter
    {
        private readonly IAccessTokenValidator _accessTokenValidator = accessTokenValidator;
        private readonly IUserReadOnlyRepository _repository = repository;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context);

                var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

                var exist = await _repository.ExistActiveUserWithIdentifier(userIdentifier);
                if (!exist)
                {
                    throw new UnauthorizedException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
                }

                // poderia fazer mais  validações do token lançando novas exceções para tratar cada caso, como token inválido, token mal formado, amdmin etc.
            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("TokenExpired")
                {
                    TokenIsExpired = true,
                });
            }
            catch (GameExchangeException ex)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
            }
        }

        private static string TokenOnRequest(AuthorizationFilterContext context)
        {
            var auth = context.HttpContext.Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(auth))
            {
                throw new UnauthorizedException(ResourceMessagesException.NO_TOKEN);
            }
            return auth["Bearer ".Length..].Trim();
        }
    }

}
