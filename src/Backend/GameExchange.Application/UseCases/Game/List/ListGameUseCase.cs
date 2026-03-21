using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories.Game;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Game.List
{
    public class ListGameUseCase(IGameReadOnlyRepository gameRepository) : IListGameUseCase
    {
        private readonly IGameReadOnlyRepository _gameRepository = gameRepository;

        public async Task<List<ResponseGameJson>> Execute()
        {
            var games = await _gameRepository.GetAll();

            return games.Adapt<List<ResponseGameJson>>();
        }
    }
}
