using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.User;
using GameExchange.Domain.Security.Cryptogaphy;
using GameExchange.Excptions;
using GameExchange.Excptions.ExceptionBase;
using Mapster;

namespace GameExchange.Application.UseCases.User
{
    public class RegisterUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository,
                                     IPasswordEncripter passwordEncripter, 
                                     IUnitOfWork unitOfWork) : IRegisterUserUseCase
    {
        public readonly IUserWriteOnlyRepository _userWriteOnlyRepository = userWriteOnlyRepository;
        public readonly IPasswordEncripter _passwordEncripter = passwordEncripter;

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = request.Adapt<Domain.Entities.User>();

            user.Password = _passwordEncripter.Encrypt(request.Password);

            await _userWriteOnlyRepository.Add(user);

            await _unitOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = request.Name
            };
       }


        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidador();

            var result = await validator.ValidateAsync(request);

            //var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);
            //if (emailExist)
            //    result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
