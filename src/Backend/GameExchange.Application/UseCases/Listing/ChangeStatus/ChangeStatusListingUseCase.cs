using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Enum;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Listing;
using GameExchange.Excptions;
using GameExchange.Excptions.ExceptionBase;
using Mapster;

namespace GameExchange.Application.UseCases.Listing.ChangeStatus
{
    public class ChangeStatusListingUseCase(IListingUpdateOnlyRepository listingUpdateOnlyRepository,
                                            IUnitOfWork unitOfWork) : IChangeStatusListingUseCase
    {
        private readonly IListingUpdateOnlyRepository _listingUpdateOnlyRepository = listingUpdateOnlyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ResponseListingJson> Execute(long id, RequestChangeStatusListing status)
        {
            await ValidateRequest(status);

            var listing =  await _listingUpdateOnlyRepository.GetById(null,id) ?? throw new NotFoundException(ResourceMessagesException.RESOURCE_NOT_FOUND);

            if (listing.Status.Equals(ListingStatus.Sold))
                throw new ErrorOnValidationException(ResourceMessagesException.ACCOUNT_IS_SOLD);

            if (listing.Status.Equals(ListingStatus.Cancelled))
                throw new ErrorOnValidationException(ResourceMessagesException.ACCOUNT_IS_CANCELLED);

            listing.Status = (ListingStatus) status.Status;

             _listingUpdateOnlyRepository.Update(listing);

            await _unitOfWork.Commit();

            return listing.Adapt<ResponseListingJson>();
        }

        private static async Task ValidateRequest(RequestChangeStatusListing status)
        {
            var validator = new ChangeStatusValidator();
            var validationResult = validator.Validate(status);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
