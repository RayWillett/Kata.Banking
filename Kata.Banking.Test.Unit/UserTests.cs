using Kata.Banking.Core.Service;
using Kata.Banking.Data;
using FluentAssertions;
using NUnit.Framework;

namespace Kata.Banking.Test.Unit
{
    public class Tests
    {
        private UserService service;
        private BankRepository repository;
        
        [SetUp]
        public void Setup()
        {
            repository = new BankRepository();
            service = new UserService(repository);
        }

        [Test]
        public void Create_Should_Add_One_User_To_Repository()
        {
            service.Create();
            repository.Users.Should().HaveCount(1);
        }
    }
}