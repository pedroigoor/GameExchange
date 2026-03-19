using GameExchange.Domain.Entities;
using GameExchange.Domain.Repositories.Token;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Infrastructe.DataAccess.Repositories
{
    public class TokenRepository(GameExchangeDbContext dbContext) : ITokenRepository
    {
        private readonly GameExchangeDbContext _dbContext = dbContext;

        public async Task<RefreshToken?> Get(string refreshToken)
        {
            return await _dbContext
                .RefreshTokens
                .AsNoTracking()
                .Include(token => token.User)
                .FirstOrDefaultAsync(token => token.Value.Equals(refreshToken));
        }

        public async Task SaveNewRefreshToken(RefreshToken refreshToken)
        {
            var tokens = _dbContext.RefreshTokens.Where(token => token.UserId == refreshToken.UserId);

            _dbContext.RefreshTokens.RemoveRange(tokens);

            await _dbContext.RefreshTokens.AddAsync(refreshToken);
        }
    }
}

