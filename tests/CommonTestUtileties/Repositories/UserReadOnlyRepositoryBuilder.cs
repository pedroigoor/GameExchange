using GameExchange.Domain.Entities;
using GameExchange.Domain.Repositories.User;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtileties.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUserReadOnlyRepository> _repository;

        public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();


        public IUserReadOnlyRepository Build() => _repository.Object;

        public void GetByEmail(User user)
        {
            _repository.Setup(r => r.GetByEmail(user.Email)).ReturnsAsync(user);

        }

        public void ExistActiveUserWithEmail(string email)
        {
            //_repository.Setup(r => r.ExistActiveUserWithEmail(It.IsAny<string>())).ReturnsAsync(exist);
            //return this;
           
        }
    }

      
 }

