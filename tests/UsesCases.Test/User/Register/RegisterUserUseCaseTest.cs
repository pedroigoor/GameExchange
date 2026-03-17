using CommonTestUtileties.Cryptography;
using CommonTestUtileties.Repositories;
using CommonTestUtileties.Requests;
using GameExchange.Application.UseCases.User.Register;
using GameExchange.Excptions;
using GameExchange.Excptions.ExceptionBase;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace UsesCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Should_Register_User_Successfully()
        {
            var userCase = CreateUseCase();
            var request = RequestRegisterUserJsonBuilder.Build();
            var result = await userCase.Execute(request);

            result.ShouldNotBeNull();
            //result.Tokens.ShouldNotBeNull();
            result.Name.ShouldBe(request.Name);
            //result.Tokens.AccessToken.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async Task Error_Email_Already_Registred()
        {

            var request = RequestRegisterUserJsonBuilder.Build();

            (var user, _) = UserBuilder.Build();
            var useCase = CreateUseCase(user);
            request.Email = user.Email;

            var exception = await Should.ThrowAsync<ErrorOnValidationException>(
                () => useCase.Execute(request)
            );

            exception.GetErrorMessages().Count.ShouldBe(1);
            exception.GetErrorMessages()
                .ShouldContain(ResourceMessagesException.EMAIL_ALREADY_REGISTERED);
        }


        [Fact]
        public async Task Error_Name_Empty()
        {

            var request = RequestRegisterUserJsonBuilder.Build();

            (var user, _) = UserBuilder.Build();
            var useCase = CreateUseCase(user);
            request.Name = string.Empty;

            var exception = await Should.ThrowAsync<ErrorOnValidationException>(
                () => useCase.Execute(request)
            );

            exception.GetErrorMessages().Count.ShouldBe(1);
            exception.GetErrorMessages()
                .ShouldContain(ResourceMessagesException.NAME_EMPTY);
        }


        [Fact]
        public async Task Error_Email_Empty()
        {

            var request = RequestRegisterUserJsonBuilder.Build();

            (var user, _) = UserBuilder.Build();
            var useCase = CreateUseCase(user);
            request.Email = string.Empty;

            var exception = await Should.ThrowAsync<ErrorOnValidationException>(
                () => useCase.Execute(request)
            );

            exception.GetErrorMessages().Count.ShouldBe(1);
            exception.GetErrorMessages()
                .ShouldContain(ResourceMessagesException.EMAIL_EMPTY);
        }

        private static RegisterUserUseCase CreateUseCase(GameExchange.Domain.Entities.User? user = null)
        {

            var writeRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            var passwordEncripter = PasswordEncripterBuilder.Build();

            if (user is not null)
                readRepositoryBuilder.GetByEmail(user);


            return new RegisterUserUseCase(writeRepository, readRepositoryBuilder.Build(), passwordEncripter, unitOfWork  );

        }
    }
}
