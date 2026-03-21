using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Listing.ChangeStatus
{
    public interface IChangeStatusListingUseCase
    {
        Task<ResponseListingJson> Execute(long id, ChangeStatusRequest status);
    }
}
