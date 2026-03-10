using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.User
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}
