using System;
using FluentAssertions;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Core.Rules;
using Kata.Banking.Data;
using NUnit.Framework;

namespace Kata.Banking.Test.Unit.Rules
{
    public class MaximumDepositRuleTests
    {
        private ITransactionRule rule;
        
        [SetUp]
        public void Setup()
        {
            rule = new MaximumDepositRule();
        }

        [Test]
        public void A_Deposit_Below_Ten_Thousand_Dollars_Should_Be_Allowed()
        {
            var result = rule.Test(0, new BankTransaction(Guid.Empty, TransactionType.CREDIT, 100));

            result.Should().BeTrue();
        }
        
        [Test]
        public void A_Deposit_Of_Ten_Thousand_Dollars_Should_Be_Allowed()
        {
            var result = rule.Test(0, new BankTransaction(Guid.Empty, TransactionType.CREDIT, 10000));

            result.Should().BeTrue();
        }
        
        
        [Test]
        public void A_Deposit_Greater_Than_Ten_Thousand_Dollars_Should_Not_Be_Allowed()
        {
            Assert.Throws<Exception>(() =>
                rule.Test(0, new BankTransaction(Guid.Empty, TransactionType.CREDIT, 10001)));
        }
        
        
        [Test]
        public void A_Withdrawal_Over_Ten_Thousand_Dollars_Should_Be_Allowed()
        {
            var result = rule.Test(0, new BankTransaction(Guid.Empty, TransactionType.DEBIT, 10001));
            
            result.Should().BeTrue();
        }
    }
}