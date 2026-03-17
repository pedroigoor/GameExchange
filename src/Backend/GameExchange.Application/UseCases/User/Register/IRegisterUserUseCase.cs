using GameExchange.Communication.Request;
using GameExchange.Communication.Response;


namespace GameExchange.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}
