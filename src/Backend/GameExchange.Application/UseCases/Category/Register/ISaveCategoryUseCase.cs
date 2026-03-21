using GameExchange.Communication.Request;
using GameExchange.Communication.Response;

namespace GameExchange.Application.UseCases.Category.Register
{
    public interface ISaveCategoryUseCase
    {
        public Task<ResponseCategoryJson> Execute(RequestCategory request);
    }
}
