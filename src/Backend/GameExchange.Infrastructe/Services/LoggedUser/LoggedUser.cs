using GameExchange.Domain.Entities;
using GameExchange.Domain.Security.Tokens;
using GameExchange.Domain.Services;
using GameExchange.Infrastructe.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameExchange.Infrastructe.Services.LoggedUser
{
    public class LoggedUser(GameExchangeDbContext context,
                            ITokenProvider tokenProvider) : ILoggedUser
    {
        private readonly GameExchangeDbContext _context = context;
        private readonly ITokenProvider _tokenProvider = tokenProvider;

        public async Task<User> User()
        {
            var tokenProvider = _tokenProvider.Value();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(tokenProvider);

            var identifier = jwtSecurityToken.Claims.First(x => x.Type == ClaimTypes.Sid).Value;

            var userIdentifier = Guid.Parse(identifier);

            return await _context
                .Users
                .AsNoTracking()
                .FirstAsync(user => user.Status && user.UserIdentifier == userIdentifier);

        }
    }

}
