using System;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Data;

namespace Kata.Banking.Core.Rules
{
    public class MaximumDepositRule: ITransactionRule
    {
        public bool Test(decimal currentBankBalance, BankTransaction candidateTransaction)
        {
            if (candidateTransaction.TransactionType == TransactionType.CREDIT &&
                candidateTransaction.Amount > 10000)
            {
                throw new Exception("You cannot deposit more than $10,000 in a single transaction.");
            }

            return true;
        }
    }
}