using GameExchange.Communication.Request;
using GameExchange.Communication.Response;

namespace GameExchange.Application.UseCases.Platform.Update
{
    public interface IUpdatePlatformUseCase
    {
        public Task<ResponsePlatformJson> Execute(long id,RequestPlatform request);
    }
}
