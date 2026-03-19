using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Entities;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Token;
using GameExchange.Domain.Repositories.User;
using GameExchange.Domain.Security.Cryptogaphy;
using GameExchange.Domain.Security.Tokens;
using GameExchange.Excptions;
using GameExchange.Excptions.ExceptionBase;
using Mapster;

namespace GameExchange.Application.UseCases.User.Register
{
    public class RegisterUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository,
                                     IUserReadOnlyRepository  userReadOnlyRepository,
                                     IPasswordEncripter passwordEncripter,
                                     IAccessTokenGenerator accessTokenGenerator,
                                     IRefreshTokenGenerator refreshTokenGenerator,
                                     ITokenRepository tokenRepository,
                                     IUnitOfWork unitOfWork) : IRegisterUserUseCase
    {
        public readonly IUserWriteOnlyRepository _userWriteOnlyRepository = userWriteOnlyRepository;
        public readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
        public readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
        public readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;
        public readonly IRefreshTokenGenerator _refreshTokenGenerator = refreshTokenGenerator;
        public readonly ITokenRepository _tokenRepository = tokenRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);

            var user = request.Adapt<Domain.Entities.User>();

            user.Password = _passwordEncripter.Encrypt(request.Password);

            await _userWriteOnlyRepository.Add(user);

            await _unitOfWork.Commit();
            var refreshToken = await CreateAndSaveRefreshToken(user);
            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                 Tokens = new ResponseTokensJson
                 {
                   AccessToken = _accessTokenGenerator.GenerateToken(user.UserIdentifier),
                   RefreshToken = refreshToken

                 }
            };
       }


        private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
        {
            var refreshToken = _refreshTokenGenerator.Generate();

            await _tokenRepository.SaveNewRefreshToken(new RefreshToken
            {
                Value = refreshToken,
                UserId = user.Id
            });

            await _unitOfWork.Commit();

            return refreshToken;
        }
        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidador();

            var result = await validator.ValidateAsync(request);

            var emailExist = await _userReadOnlyRepository.GetByEmail(request.Email);
            if (emailExist is not null)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
