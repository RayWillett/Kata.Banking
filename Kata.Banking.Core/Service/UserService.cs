using System;
using System.Linq;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Data;

namespace Kata.Banking.Core.Service
{
    public class UserService: IManageUsers
    {
        private BankRepository repository;
        public UserService(BankRepository _repository)
        {
            repository = _repository;
        }

        public User Create()
        {
            var user = new User();
            repository.Users.Add(user);
            return user;
        }
    }
}