using System;
using FluentAssertions;
using Kata.Banking.Core.Service;
using Kata.Banking.Data;
using NUnit.Framework;

namespace Kata.Banking.Test.Unit
{
    public class BankAccountBalanceTests
    {
        private BankAccountService _accountService;
        private BankRepository repository;
        
        [SetUp]
        public void Setup()
        {
            repository = new BankRepository();
            _accountService = new BankAccountService(repository);
        }

        [Test]
        public void Creating_A_Bank_Account_Manually_Has_A_Balance_Of_0()
        {
            var userId = Guid.NewGuid();
            var bankAccount = new BankAccount(userId);
            repository.BankAccounts.Add(bankAccount);
            
            _accountService.GetBalance(bankAccount.ID).Should().Be(0);
        }

        [Test]
        public void Getting_Balance_Of_New_Account_Returns_100()
        {
            var userId = Guid.NewGuid();
            var bankAccount = _accountService.Create(userId);

            _accountService.GetBalance(bankAccount.ID).Should().Be(100);
        }

        [Test]
        public void Getting_Balance_With_One_Credit_Should_Return_Amount_Of_Credit()
        {
            var bankAccount = _accountService.Create(Guid.NewGuid());
            
            repository.BankTransactions.Add(new BankTransaction(bankAccount.ID, TransactionType.CREDIT, 100));

            _accountService.GetBalance(bankAccount.ID).Should().Be(200);
        }
        
        [Test]
        public void Getting_Balance_With_One_Credit_And_One_Debit_Should_Return_Amount_Of_Credit_Minus_Debit()
        {
            var bankAccount = _accountService.Create(Guid.NewGuid());
            
            repository.BankTransactions.Add(new BankTransaction(bankAccount.ID, TransactionType.CREDIT, 100));
            repository.BankTransactions.Add(new BankTransaction(bankAccount.ID, TransactionType.DEBIT, 10.50m));

            _accountService.GetBalance(bankAccount.ID).Should().Be(189.50m);
        }
    }
}