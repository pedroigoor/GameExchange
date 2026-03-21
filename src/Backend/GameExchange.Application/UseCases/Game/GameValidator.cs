using FluentValidation;
using GameExchange.Communication.Request;
using GameExchange.Excptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Game
{
    public class GameValidator : AbstractValidator<RequestGame>
    {

        public GameValidator()
        {
            RuleFor(RuleFor => RuleFor.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(RuleFor => RuleFor.PlatformId).NotNull().WithMessage(ResourceMessagesException.PLATFORM_EMPTY);
            RuleFor(RuleFor => RuleFor.CategoryId).NotNull().WithMessage(ResourceMessagesException.CATEGORY_EMPTY);
        }
    }
}
