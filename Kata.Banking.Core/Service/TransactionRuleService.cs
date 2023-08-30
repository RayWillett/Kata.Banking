using System;
using System.Collections.Generic;
using System.Linq;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Data;

namespace Kata.Banking.Core.Service
{
    public class TransactionRuleService: ITransactionRuleEngine
    {
        private IEnumerable<ITransactionRule> transactionRules;
        
        public TransactionRuleService(IEnumerable<ITransactionRule> _transactionRules)
        {
            transactionRules = _transactionRules;
        }

        public bool ApplyRules(decimal currentBankBalance, BankTransaction candidateTransaction)
        {
            return transactionRules
                .Select(rule => rule.Test(currentBankBalance, candidateTransaction))
                .All(val => val);
        }
    }
}