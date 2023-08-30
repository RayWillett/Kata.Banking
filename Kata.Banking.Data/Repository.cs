using System.Collections.Generic;

namespace Kata.Banking.Data
{
    public class BankRepository
    {
        public BankRepository()
        {
            BankAccounts = new List<BankAccount>();
            Users = new List<User>();
            BankTransactions = new List<BankTransaction>();
        }

        public List<BankAccount> BankAccounts { get; set; }
        public List<User> Users { get; set; }
        public List<BankTransaction> BankTransactions { get; set; }
    }
}