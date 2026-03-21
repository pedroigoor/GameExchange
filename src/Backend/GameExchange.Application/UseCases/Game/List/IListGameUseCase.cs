using GameExchange.Communication.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Game.List
{
    public interface IListGameUseCase
    {
        Task<List<ResponseGameJson>> Execute();
    }
}
