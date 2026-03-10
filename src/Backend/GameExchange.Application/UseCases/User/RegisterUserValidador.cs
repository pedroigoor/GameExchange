using FluentValidation;
using GameExchange.Application.SharedValidators;
using GameExchange.Communication.Request;
using GameExchange.Excptions;

namespace GameExchange.Application.UseCases.User
{
    public class RegisterUserValidador : AbstractValidator<RequestRegisterUserJson>
    {

        public RegisterUserValidador()
        {
            RuleFor(RuleFor => RuleFor.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(RuleFor => RuleFor.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
            RuleFor(RuleFor => RuleFor.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
            When(RuleFor => !string.IsNullOrEmpty(RuleFor.Email), () =>
            {
                RuleFor(RuleFor => RuleFor.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
            });


        }

    }
}
