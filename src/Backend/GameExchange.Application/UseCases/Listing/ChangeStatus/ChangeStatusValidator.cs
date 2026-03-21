using FluentValidation;
using GameExchange.Communication.Request;
using GameExchange.Excptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Listing.ChangeStatus
{
    public class ChangeStatusValidator : AbstractValidator<ChangeStatusRequest>
    {
        public ChangeStatusValidator()
        {
            RuleFor(r => r.Status).IsInEnum().WithMessage(ResourceMessagesException.STATUS_LISTING_NOT_SUPPORTED);
        }
    }
}

