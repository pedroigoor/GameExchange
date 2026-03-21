using GameExchange.Communication.Response;

namespace GameExchange.Application.UseCases.Platform.List
{
    public interface IListPlatformUseCase
    {
        Task<List<ResponsePlatformJson>> Execute();
    }
}
