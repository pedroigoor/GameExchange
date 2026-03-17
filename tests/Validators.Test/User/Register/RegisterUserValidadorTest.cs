using CommonTestUtileties.Requests;
using GameExchange.Application.UseCases.User.Register;
using GameExchange.Excptions;
using Shouldly;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidadorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.ShouldBeTrue();

        }

        [Fact]
        public void Name_Is_Empty()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);

            result.Errors.ShouldContain(e =>
                e.ErrorMessage == ResourceMessagesException.NAME_EMPTY);

        }

        [Fact]
        public void Email_Is_Empty()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);

            result.Errors.ShouldContain(e =>
                e.ErrorMessage == ResourceMessagesException.EMAIL_EMPTY);

        }

        [Fact]
        public void Email_Is_Invalid()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email ="165165";

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);

            result.Errors.ShouldContain(e =>
                e.ErrorMessage == ResourceMessagesException.EMAIL_INVALID);

        }

        [Fact]
        public void Pass_Is_Invalid()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Password = "12";

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);

            result.Errors.ShouldContain(e =>
                e.ErrorMessage == ResourceMessagesException.PASSWORD_INVALID);

        }
        [Fact]
        public void Pass_Is_Empty()
        {
            var validator = new RegisterUserValidador();

            var request = RequestRegisterUserJsonBuilder.Build();
            request.Password = "";

            var result = validator.Validate(request);

            result.IsValid.ShouldBeFalse();
            result.Errors.Count.ShouldBe(1);

            result.Errors.ShouldContain(e =>
                e.ErrorMessage == ResourceMessagesException.PASSWORD_EMPTY);

        }
    }
}

