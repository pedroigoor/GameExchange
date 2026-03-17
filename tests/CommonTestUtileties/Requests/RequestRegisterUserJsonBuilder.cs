using Bogus;
using GameExchange.Communication.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtileties.Requests
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build(int passwordLength = 10)
        {
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(user => user.Name, f => f.Person.FirstName)
                .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
                .RuleFor(user => user.Password, f => f.Internet.Password(passwordLength))
                .Generate();
        }
    }
}
