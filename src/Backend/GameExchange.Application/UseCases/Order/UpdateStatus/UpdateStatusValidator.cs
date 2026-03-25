using FluentValidation;
using GameExchange.Communication.Request;
using GameExchange.Excptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Order.UpdateStatus
{
    internal class UpdateStatusValidator:AbstractValidator<RequestChangeStatusOrder>
    {
        public UpdateStatusValidator()
        {
            RuleFor(r => r.Status).IsInEnum().WithMessage(ResourceMessagesException.STATUS_NOT_SUPPORTED);
        }
    }
}
