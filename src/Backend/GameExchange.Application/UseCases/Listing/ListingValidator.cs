using FluentValidation;
using GameExchange.Communication.Request;
using GameExchange.Excptions;

namespace GameExchange.Application.UseCases.Listing
{
    public class ListingValidator :AbstractValidator<RequestListing>
    {
        public ListingValidator()
        {
            RuleFor(listing=> listing.Title).NotEmpty().WithMessage(ResourceMessagesException.LISTING_TITLE_EMPTY);
            RuleFor(listing=> listing.Description).NotEmpty().WithMessage(ResourceMessagesException.LISTING_DESCRIPTION_EMPTY);
            RuleFor(listing=> listing.Price).GreaterThan(0).WithMessage(ResourceMessagesException.LISTING_PRICE_ZERO_OR_NEGATIVE);
            RuleFor(listing => listing.GameId).GreaterThan(0).WithMessage(ResourceMessagesException.LISTING_GAME_EMPTY);
        }
    }
}
