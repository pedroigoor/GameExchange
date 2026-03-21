using GameExchange.Communication.Request;
using GameExchange.Communication.Response;

namespace GameExchange.Application.UseCases.Category.Update
{
    public interface IUpdateCategoryUseCase
    {
        public Task<ResponseCategoryJson> Execute(long id, RequestCategory request);
    }
}
