using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Platform;
using GameExchange.Excptions;
using GameExchange.Excptions.ExceptionBase;
using Mapster;

namespace GameExchange.Application.UseCases.Platform.Update
{
    public class UpdatePlatformUseCase(IPlatformUpdateOnlyRepository repository, IUnitOfWork unitOfWork) : IUpdatePlatformUseCase
    {
        public readonly IPlatformUpdateOnlyRepository _repository = repository;
        public readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ResponsePlatformJson> Execute(long id, RequestPlatform request)
        {
            await Validate(request);

            var platform = await _repository.GetById(id) ?? throw new NotFoundException(ResourceMessagesException.RESOURCE_NOT_FOUND);

            request.Adapt(platform);

            _repository.Update(platform);

            await _unitOfWork.Commit();

            return platform.Adapt<ResponsePlatformJson>();
        }

        private static async Task Validate(RequestPlatform request)
        {
            var result = new SavePlatformValidator().Validate(request);

            if (!result.IsValid)
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
        }
    }
}
