using System;

namespace Kata.Banking.Data
{
    public class BankAccount
    {
        public BankAccount(Guid userId)
        {
            ID = Guid.NewGuid();
            UserID = userId;
        }

        public Guid ID { get; private set; }
        public Guid UserID { get; private set; }
    }
}