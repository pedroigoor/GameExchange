
using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Enum;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Listing;
using GameExchange.Domain.Services;
using GameExchange.Excptions.ExceptionBase;
using Mapster;

namespace GameExchange.Application.UseCases.Listing.Register
{
    public class SaveListingUseCase(ILoggedUser loggedUser,
                                    IListingWriteOnlyRepository listingWriteOnlyRepository,
                                    IUnitOfWork unitOfWork) : ISaveListingUseCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IListingWriteOnlyRepository _listingWriteOnlyRepository = listingWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ResponseListingJson> Execute(RequestListing requestListing)
        {
            await ValidateRequest(requestListing);

            var user = await _loggedUser.User();

            var listing = requestListing.Adapt<Domain.Entities.Listing>();

            listing.Status =  ListingStatus.Draft; 

            listing.SellerId = user.Id;

            await _listingWriteOnlyRepository.Add(listing);

            await _unitOfWork.Commit();

            return listing.Adapt<ResponseListingJson>();
        }

        private static async Task ValidateRequest(RequestListing requestListing)
        {
            var validate = new ListingValidator(); 
            var result = await validate.ValidateAsync(requestListing);

            if (!result.IsValid) {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
