using GameExchange.Domain.Security.Cryptogaphy;
using GameExchange.Infrastructe.Security.Cryptogaphy;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtileties.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static IPasswordEncripter Build() => new BCryptNet();
    }
}
