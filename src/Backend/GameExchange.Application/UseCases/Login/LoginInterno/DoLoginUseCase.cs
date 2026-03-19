using GameExchange.Communication.Request;
using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Token;
using GameExchange.Domain.Repositories.User;
using GameExchange.Domain.Security.Cryptogaphy;
using GameExchange.Domain.Security.Tokens;
using GameExchange.Excptions.ExceptionBase;

namespace GameExchange.Application.UseCases.Login.LoginInterno
{
    public class DoLoginUseCase(
        IUserReadOnlyRepository repository,
        IAccessTokenGenerator accessTokenGenerator,
        IPasswordEncripter passwordEncripter,
        IRefreshTokenGenerator refreshTokenGenerator,
        ITokenRepository tokenRepository,
        IUnitOfWork unitOfWork) : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _repository = repository;
        private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator = refreshTokenGenerator;
        private readonly ITokenRepository _tokenRepository = tokenRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {
            var user = await _repository.GetByEmail(request.Email);

            if (user is null || !_passwordEncripter.IsValid(request.PassWord, user.Password))
                throw new InvalidLoginException();

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
            var refreshToken = new Domain.Entities.RefreshToken
            {
                Value = _refreshTokenGenerator.Generate(),
                UserId = user.Id
            };

            await _tokenRepository.SaveNewRefreshToken(refreshToken);

            await _unitOfWork.Commit();

            return refreshToken.Value;
        }
    }
}
