using System;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Data;

namespace Kata.Banking.Core.Rules
{
    public class MinimumAccountBalanceRule: ITransactionRule
    {
        public bool Test(decimal currentBankBalance, BankTransaction candidateTransaction)
        {
            if (candidateTransaction.TransactionType == TransactionType.DEBIT &&
                (currentBankBalance - candidateTransaction.Amount) < 100)
            {
                throw new Exception("The account's balance must remain above $100.");
            }

            return true;
        }
    }
}