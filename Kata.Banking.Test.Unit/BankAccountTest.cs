using System;
using System.Linq;
using Kata.Banking.Data;
using NUnit.Framework;
using Kata.Banking.Core.Service;
using FluentAssertions;

namespace Kata.Banking.Test.Unit
{
    public class BankAccountTest
    {
        private BankAccountService accountService;
        private BankRepository repository;
        private UserService userService;
        
        [SetUp]
        public void Setup()
        {
            repository = new BankRepository();
            accountService = new BankAccountService(repository);
            userService = new UserService(repository);
        }
        
        [Test]
        public void Create_Should_Add_One_Bank_Account_To_Repository()
        {
            var userId = Guid.Empty;
            accountService.Create(userId);

            repository.BankAccounts.Should().HaveCount(1);
        }

        [Test]
        public void Created_Bank_Account_Should_Have_User_ID()
        {
            var userId = Guid.NewGuid();
            accountService.Create(userId);

            repository.BankAccounts.First().UserID.Should().Be(userId);
        }

        [Test]
        public void Delete_Bank_Account_Should_Remove_One_Account()
        {
            var userId = Guid.Empty;
            accountService.Create(userId);
            accountService.Create(userId);
            var accountToRemove = accountService.Create(userId);
            repository.BankAccounts.Should().HaveCount(3);

            accountService.Delete(accountToRemove.ID);
            
            repository.BankAccounts.Should().HaveCount(2);
            repository.BankAccounts.Should().NotContain(accountToRemove);
        }

        [Test]
        public void Getting_A_List_Of_Accounts_For_A_Nonexistent_User_Returns_Empty_List()
        {
            var accounts = accountService.ListByUser(Guid.Empty);

            accounts.Should().BeEmpty();
        }
        
        [Test]
        public void Getting_A_List_Of_Accounts_For_A_User_With_No_Accounts_Returns_Empty_List()
        {
            var user = userService.Create();
            
            var accounts = accountService.ListByUser(user.ID);

            accounts.Should().BeEmpty();
        }
        
        [Test]
        public void Getting_A_List_Of_Accounts_For_A_User_With_One_Account_Returns_List_With_One_Account()
        {
            var user = userService.Create();
            accountService.Create(user.ID);
            
            var accounts = accountService.ListByUser(user.ID);

            accounts.Should().HaveCount(1);
            accounts.First().UserID.Should().Be(user.ID);
        }

        [Test]
        public void Getting_A_List_Of_Accounts_For_A_User_With_One_Account_Returns_List_With_Only_Their_Account()
        {
            var user1 = userService.Create();
            var user2 = userService.Create();
            accountService.Create(user1.ID);
            accountService.Create(user2.ID);
            
            var accounts = accountService.ListByUser(user1.ID);

            accounts.Should().HaveCount(1);
            accounts.First().UserID.Should().Be(user1.ID);
        }

        [Test]
        public void Getting_A_List_Of_Accounts_For_A_User_With_Two_Accounts_Returns_List_With_Only_Their_Accounts()
        {
            var user1 = userService.Create();
            var user2 = userService.Create();
            accountService.Create(user1.ID);
            accountService.Create(user1.ID);
            accountService.Create(user2.ID);
            
            var accounts = accountService.ListByUser(user1.ID);

            accounts.Should().HaveCount(2);
            accounts.First().UserID.Should().Be(user1.ID);
            accounts.Last().UserID.Should().Be(user1.ID);
        }
    }
}