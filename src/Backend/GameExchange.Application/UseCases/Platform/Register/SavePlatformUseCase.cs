using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Platform;
using GameExchange.Excptions.ExceptionBase;
using Mapster;

namespace GameExchange.Application.UseCases.Platform.Register
{
    public class SavePlatformUseCase( IPlatformWriteOnlyRepository platformWriteOnlyRepository,
                                     IUnitOfWork unitOfWork) : ISavePlatformUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPlatformWriteOnlyRepository _platformWriteOnlyRepository = platformWriteOnlyRepository;

        public async Task<ResponsePlatformJson> Execute(RequestPlatform request)
        {
            await Validate(request);

            var platform = request.Adapt<Domain.Entities.Platform>();

            await _platformWriteOnlyRepository.Add(platform);

            await _unitOfWork.Commit();

            return platform.Adapt<ResponsePlatformJson>();


        }

        private static async Task Validate(RequestPlatform request)
        {
            var validator = new PlatformValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
