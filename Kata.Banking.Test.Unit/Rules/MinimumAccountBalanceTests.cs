using System;
using FluentAssertions;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Core.Rules;
using Kata.Banking.Data;
using NUnit.Framework;

namespace Kata.Banking.Test.Unit.Rules
{
    public class MinimumAccountBalanceTests
    {
        private ITransactionRule rule;
        
        [SetUp]
        public void Setup()
        {
            rule = new MinimumAccountBalanceRule();
        }

        [Test]
        public void An_Account_Be_Withdrawn_From_If_The_Balance_Remains_At_One_Hundred_Dollars()
        {
            var result = rule.Test(101, new BankTransaction(Guid.Empty, TransactionType.DEBIT, 1));

            result.Should().BeTrue();
        }
        
        [Test]
        public void An_Account_Be_Withdrawn_From_If_The_Balance_Remains_Above_One_Hundred_Dollars()
        {
            var result = rule.Test(102, new BankTransaction(Guid.Empty, TransactionType.DEBIT, 1));

            result.Should().BeTrue();
        }
        
        [Test]
        public void An_Account_Can_Not_Be_Withdrawn_From_If_The_Balance_Would_Drop_Below_One_Hundred_Dollars()
        {
            Assert.Throws<Exception>(() => 
                rule.Test(100, new BankTransaction(Guid.Empty, TransactionType.DEBIT, 1)));
        }
        
        
        [Test]
        public void An_Account_Be_Debited_To_If_The_Balance_Remains_At_One_Hundred_Dollars()
        {
            var result = rule.Test(100, new BankTransaction(Guid.Empty, TransactionType.CREDIT, 1));

            result.Should().BeTrue();
        }
    }
}