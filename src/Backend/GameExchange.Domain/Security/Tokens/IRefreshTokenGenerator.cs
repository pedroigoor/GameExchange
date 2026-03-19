using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Domain.Security.Tokens
{
    public interface IRefreshTokenGenerator
    {
        public string Generate();
    }
}

