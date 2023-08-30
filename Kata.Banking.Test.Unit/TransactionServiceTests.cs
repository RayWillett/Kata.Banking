using System;
using System.Linq;
using Kata.Banking.Core.Service;
using Kata.Banking.Data;
using FluentAssertions;
using Kata.Banking.Core.Abstractions;
using Moq;
using NUnit.Framework;

namespace Kata.Banking.Test.Unit
{
    public class TransactionServiceTests
    {
        private TransactionService service;
        private BankRepository repository;
        private Mock<IManageBankAccounts> mockBankService;
        private Mock<ITransactionRuleEngine> mockRulesEngine;
        
        [SetUp]
        public void Setup()
        {
            mockBankService = new Mock<IManageBankAccounts>();
            mockRulesEngine = new Mock<ITransactionRuleEngine>();
            repository = new BankRepository();
            service = new TransactionService(repository, mockBankService.Object, mockRulesEngine.Object);
        }

        [Test]
        public void DebitAccount_Should_Add_One_Debit_Transaction_To_Repository()
        {
            mockBankService.Setup(ctx => ctx.GetBalance(It.IsAny<Guid>())).Returns(100);
            mockRulesEngine.Setup(ctx => 
                    ctx.ApplyRules(It.IsAny<decimal>(), It.IsAny<BankTransaction>()))
                .Returns(true);
            var accountId = Guid.Empty;
            
            service.DebitAccount(accountId, 0);
            
            repository.BankTransactions.Should().HaveCount(1);
            repository.BankTransactions.First().TransactionType.Should().Be(TransactionType.DEBIT);
        }
        
        [Test]
        public void CreditAccount_Should_Add_One_Credit_Transaction_To_Repository()
        {
            mockBankService.Setup(ctx => ctx.GetBalance(It.IsAny<Guid>())).Returns(100);
            mockRulesEngine.Setup(ctx => 
                    ctx.ApplyRules(It.IsAny<decimal>(), It.IsAny<BankTransaction>()))
                .Returns(true);
            var accountId = Guid.Empty;
            
            service.CreditAccount(accountId, 0);
            
            repository.BankTransactions.Should().HaveCount(1);
            repository.BankTransactions.First().TransactionType.Should().Be(TransactionType.CREDIT);
        }
        
        [Test]
        public void Adding_A_Transaction_Should_Include_The_Provided_Bank_Account_ID()
        {
            mockBankService.Setup(ctx => ctx.GetBalance(It.IsAny<Guid>())).Returns(100);
            mockRulesEngine.Setup(ctx => 
                    ctx.ApplyRules(It.IsAny<decimal>(), It.IsAny<BankTransaction>()))
                .Returns(true);
            var accountId = Guid.NewGuid();
            
            service.CreditAccount(accountId, 0);
            
            repository.BankTransactions.Should().HaveCount(1);
            repository.BankTransactions.First().AccountID.Should().Be(accountId);
        }
        
        [Test]
        public void Adding_A_Transaction_Should_Include_An_Amount()
        {
            mockBankService.Setup(ctx => ctx.GetBalance(It.IsAny<Guid>())).Returns(100);
            mockRulesEngine.Setup(ctx => 
                    ctx.ApplyRules(It.IsAny<decimal>(), It.IsAny<BankTransaction>()))
                .Returns(true);
            var accountId = Guid.NewGuid();
            var amount = 3.14m;
            
            service.CreditAccount(accountId, amount);
            
            repository.BankTransactions.Should().HaveCount(1);
            repository.BankTransactions.First().Amount.Should().Be(amount);
        }
        
        [Test]
        public void If_Rules_Engine_Fails_Transaction_Should_Not_Be_Added()
        {
            mockBankService.Setup(ctx => ctx.GetBalance(It.IsAny<Guid>())).Returns(100);
            mockRulesEngine.Setup(ctx => 
                    ctx.ApplyRules(It.IsAny<decimal>(), It.IsAny<BankTransaction>()))
                .Throws(new Exception("Some exception"));
            var accountId = Guid.NewGuid();
            var amount = 3.14m;

            Assert.Throws<Exception>(() => service.CreditAccount(accountId, amount));
            
            repository.BankTransactions.Should().HaveCount(0);
        }
    }
}