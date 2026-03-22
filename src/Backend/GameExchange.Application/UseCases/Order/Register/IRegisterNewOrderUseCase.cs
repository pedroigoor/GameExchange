using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Entities;

namespace GameExchange.Application.UseCases.Order.Register
{
    public interface IRegisterNewOrderUseCase
    {
        Task<ResponseOrderJson> Execute(long listingId);
    }
}
