using System;
using System.Collections.Generic;
using FluentAssertions;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Core.Service;
using Kata.Banking.Data;
using Moq;
using NUnit.Framework;

namespace Kata.Banking.Test.Unit
{
    public class TransactionRulesTests
    {
        [Test]
        public void Rules_Engine_Applies_At_Least_One_Rule()
        {
            var mockTransactionRule = new Mock<ITransactionRule>();
            var transactionRuleService = new TransactionRuleService(new List<ITransactionRule>
            {
                mockTransactionRule.Object
            });

            var bankBalance = 0;
            var transaction = new BankTransaction(default, default, default);
            transactionRuleService.ApplyRules(bankBalance, transaction);
            
            mockTransactionRule.Verify(txRule => txRule.Test(bankBalance, transaction));
        }
        
        [Test]
        public void Rules_Engine_Applies_All_Rules()
        {
            var mockTransactionRule1 = new Mock<ITransactionRule>();
            var mockTransactionRule2 = new Mock<ITransactionRule>();
            var mockTransactionRule3 = new Mock<ITransactionRule>();
            mockTransactionRule1
                .Setup(rule => 
                    rule.Test(It.IsAny<decimal>(), It.IsAny<BankTransaction>()))
                .Returns(true);
            mockTransactionRule2
                .Setup(rule => 
                    rule.Test(It.IsAny<decimal>(), It.IsAny<BankTransaction>()))
                .Returns(true);
            mockTransactionRule3
                .Setup(rule => 
                    rule.Test(It.IsAny<decimal>(), It.IsAny<BankTransaction>()))
                .Returns(true);
            
            var transactionRuleService = new TransactionRuleService(new List<ITransactionRule>
            {
                mockTransactionRule1.Object,
                mockTransactionRule2.Object,
                mockTransactionRule3.Object
            });

            var bankBalance = 0;
            var transaction = new BankTransaction(default, default, default);
            transactionRuleService.ApplyRules(bankBalance, transaction);
            
            mockTransactionRule1.Verify(txRule => txRule.Test(bankBalance, transaction));
            mockTransactionRule2.Verify(txRule => txRule.Test(bankBalance, transaction));
            mockTransactionRule3.Verify(txRule => txRule.Test(bankBalance, transaction));
        }
    }
}