using GameExchange.Domain.Repositories.User;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtileties.Repositories
{
    public class UserWriteOnlyRepositoryBuilder
    {
        public static IUserWriteOnlyRepository Build()
        {
            var mock = new Mock<IUserWriteOnlyRepository>();

            return mock.Object;
        }
    }
}
