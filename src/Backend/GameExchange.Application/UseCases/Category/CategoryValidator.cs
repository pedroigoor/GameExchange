using FluentValidation;
using GameExchange.Communication.Request;
using GameExchange.Excptions;

namespace GameExchange.Application.UseCases.Category
{
    public class CategoryValidator : AbstractValidator<RequestCategory>
    {

        public CategoryValidator()
        {
            RuleFor(RuleFor => RuleFor.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        }
    }
}
