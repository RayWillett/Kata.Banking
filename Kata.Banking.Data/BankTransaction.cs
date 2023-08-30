using System;

namespace Kata.Banking.Data
{
    public enum TransactionType: int
    {
        CREDIT = 0,
        DEBIT
    }

    public class BankTransaction
    {
        public BankTransaction(Guid accountID, TransactionType transactionType, decimal amount)
        {
            ID = Guid.NewGuid();
            AccountID = accountID;
            TransactionType = transactionType;
            Amount = amount;
        }

        public Guid ID { get; private set; }
        public Guid AccountID { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public decimal Amount { get; private set;}
    }
}