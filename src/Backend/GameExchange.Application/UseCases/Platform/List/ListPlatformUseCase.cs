using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories.Platform;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Platform.List
{
    public class ListPlatformUseCase(IPlatformReadOnlyRepository platformReadOnlyRepository) : IListPlatformUseCase
    {
        private readonly IPlatformReadOnlyRepository _platformReadOnlyRepository = platformReadOnlyRepository;

        public async Task<List<ResponsePlatformJson>> Execute()
        {
            var platforms = await _platformReadOnlyRepository.GetAll();

            return platforms.Adapt<List<ResponsePlatformJson>>();
        }
    }
}
