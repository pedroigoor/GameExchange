using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Game.Register
{
    public interface ISaveGameUseCase
    {
        public Task<ResponseGameJson> Execute(RequestGame request);
    }
}
