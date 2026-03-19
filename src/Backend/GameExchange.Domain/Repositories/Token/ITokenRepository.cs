using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Domain.Repositories.Token
{
    public interface ITokenRepository
    {
        Task<Entities.RefreshToken?> Get(string refreshToken);
        Task SaveNewRefreshToken(Entities.RefreshToken refreshToken);
    }

}
