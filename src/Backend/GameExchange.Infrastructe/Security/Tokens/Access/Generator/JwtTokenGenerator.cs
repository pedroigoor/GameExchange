using GameExchange.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GameExchange.Infrastructe.Security.Tokens.Access.Generator
{
    public class JwtTokenGenerator(uint expirationTimeMinutes, string secretKey) : JwtTokenHandler, IAccessTokenGenerator
    {
        private readonly uint _expirationTimeMinutes = expirationTimeMinutes;
        private readonly string _secretKey = secretKey;

        public string GenerateToken(Guid userIntentifier)
        {
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Sid, userIntentifier.ToString())
                ]),
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(_secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(securityTokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
    }
}
