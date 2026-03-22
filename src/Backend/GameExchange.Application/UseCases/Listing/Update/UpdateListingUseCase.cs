using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Enum;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Listing;
using GameExchange.Domain.Services;
using GameExchange.Excptions;
using GameExchange.Excptions.ExceptionBase;
using Mapster;

namespace GameExchange.Application.UseCases.Listing.Update
{
    public class UpdateListingUseCase(IListingUpdateOnlyRepository ListingUpdateOnlyRepositor,
                                      IUnitOfWork UnitOfWork,
                                      ILoggedUser loggedUser) : IUpdateListingUseCase
    {
        private readonly IListingUpdateOnlyRepository _ListingUpdateOnlyRepository = ListingUpdateOnlyRepositor;
        private readonly IUnitOfWork _unitOfWork = UnitOfWork;
        private readonly ILoggedUser _loggedUser = loggedUser;
        public  async Task<ResponseListingJson> Execute(long id, RequestListing request)
        {
            await Validate(request);

            var user = await _loggedUser.User();


            var listing = await _ListingUpdateOnlyRepository.GetById(user, id) ?? throw new NotFoundException(ResourceMessagesException.RESOURCE_NOT_FOUND);

            if (listing.Status.Equals(ListingStatus.Sold))
                throw new ErrorOnValidationException(ResourceMessagesException.ACCOUNT_IS_SOLD);

            if (listing.Status.Equals(ListingStatus.Cancelled))
                throw new ErrorOnValidationException(ResourceMessagesException.ACCOUNT_IS_CANCELLED);


            request.Adapt(listing);

            _ListingUpdateOnlyRepository.Update(listing);
            await _unitOfWork.Commit();

            return listing.Adapt<ResponseListingJson>();


        }

        private static async Task Validate(RequestListing request)
        {
            var validator = new ListingValidator();

            var result = await validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }


        }
    }
}
