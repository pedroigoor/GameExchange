using FluentValidation;
using GameExchange.Communication.Request;
using GameExchange.Excptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Platform
{
    public class PlatformValidator  : AbstractValidator<RequestPlatform>
    {

        public PlatformValidator()
        {
            RuleFor(RuleFor => RuleFor.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        }
    }
}
