using System;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Data;

namespace Kata.Banking.Core.Rules
{
    public class MaximumPercentageWithdrawnRule: ITransactionRule
    {
        public bool Test(decimal currentBankBalance, BankTransaction candidateTransaction)
        {
            if (candidateTransaction.TransactionType == TransactionType.DEBIT  &&
                (candidateTransaction.Amount / currentBankBalance) > .90m)
            {
                throw new Exception("You cannot withdraw over 90% of your account balance in a single transaction");
            }

            return true;
        }
    }
}