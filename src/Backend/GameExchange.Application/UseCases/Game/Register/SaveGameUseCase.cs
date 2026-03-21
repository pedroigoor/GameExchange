using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Game;
using GameExchange.Excptions.ExceptionBase;
using Mapster;

namespace GameExchange.Application.UseCases.Game.Register
{
    public class SaveGameUseCase(IGameWriteOnlyRepository gameWriteOnlyRepository,
                                 IUnitOfWork unitOfWork) : ISaveGameUseCase
    {
        private readonly IUnitOfWork _unitOfWork= unitOfWork;
        private readonly IGameWriteOnlyRepository _gameWriteOnlyRepository = gameWriteOnlyRepository;

        public async Task<ResponseGameJson> Execute(RequestGame request)
        {
            await Validate(request);

            var game = request.Adapt<GameExchange.Domain.Entities.Game>();

            await _gameWriteOnlyRepository.Add(game);

            await _unitOfWork.Commit();
            
            return game.Adapt<ResponseGameJson>();
        }

        private static async Task Validate(RequestGame request)
        {
            var validator = new GameValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
