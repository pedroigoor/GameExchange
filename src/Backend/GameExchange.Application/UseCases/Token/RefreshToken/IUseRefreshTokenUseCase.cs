using GameExchange.Communication.Request;
using GameExchange.Communication.Response;

namespace GameExchange.Application.UseCases.Token.RefreshToken
{
    public interface IUseRefreshTokenUseCase
    {
        Task<ResponseTokensJson> Execute(RequestNewTokenJson request);
    }
}
