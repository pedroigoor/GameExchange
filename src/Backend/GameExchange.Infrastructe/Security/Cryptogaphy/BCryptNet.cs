using GameExchange.Domain.Security.Cryptogaphy;
using BCrypt.Net;

namespace GameExchange.Infrastructe.Security.Cryptogaphy
{
    public class BCryptNet : IPasswordEncripter
    {

        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool IsValid(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}

