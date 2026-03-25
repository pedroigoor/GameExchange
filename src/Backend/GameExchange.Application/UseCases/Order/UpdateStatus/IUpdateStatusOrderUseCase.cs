using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Order.UpdateStatus
{
    public interface IUpdateStatusOrderUseCase
    {
        Task<ResponseOrderJson> Execute(long idOrder, RequestChangeStatusOrder status);
    }
}
