using GameExchange.Communication.Response;

namespace GameExchange.Application.UseCases.Category.List
{
    public interface IListCategoryUseCase
    {
        Task<List<ResponseCategoryJson>> Execute();
    }
}
