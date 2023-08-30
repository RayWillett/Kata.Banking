using System;
using FluentAssertions;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Core.Rules;
using Kata.Banking.Data;
using NUnit.Framework;

namespace Kata.Banking.Test.Unit.Rules
{
    public class MaximumPercentageWithdrawnRuleTests
    {
        private ITransactionRule rule;
        
        [SetUp]
        public void Setup()
        {
            rule = new MaximumPercentageWithdrawnRule();
        }

        [Test]
        public void A_Withdrawal_Of_50_Percent_Should_Be_Allowed()
        {
            var result = rule.Test(100, new BankTransaction(Guid.Empty, TransactionType.DEBIT, 50));

            result.Should().BeTrue();
        }
        
        [Test]
        public void A_Deposit_Of_100_Percent_Should_Be_Allowed()
        {
            var result = rule.Test(100, new BankTransaction(Guid.Empty, TransactionType.CREDIT, 100));

            result.Should().BeTrue();
        }
        
        [Test]
        public void A_Withdrawal_Over_90_Percent_Should_Not_Be_Allowed()
        {
            Assert.Throws<Exception>(() =>
                rule.Test(100, new BankTransaction(Guid.Empty, TransactionType.DEBIT, 91)));
        }
    }
}