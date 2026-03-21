using GameExchange.Communication.Request;
using GameExchange.Communication.Response;

namespace GameExchange.Application.UseCases.Platform.Register
{
    public interface ISavePlatformUseCase
    {

        public Task<ResponsePlatformJson> Execute(RequestPlatform request);
    }
}
