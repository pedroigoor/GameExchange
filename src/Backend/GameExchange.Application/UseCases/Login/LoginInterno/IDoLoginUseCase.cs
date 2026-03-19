using GameExchange.Communication.Request;
using GameExchange.Communication.Response;

namespace GameExchange.Application.UseCases.Login.LoginInterno
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
    }
}
