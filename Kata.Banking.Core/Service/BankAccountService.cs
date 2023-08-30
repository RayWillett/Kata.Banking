using System;
using System.Collections.Generic;
using System.Linq;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Data;

namespace Kata.Banking.Core.Service
{
    public class BankAccountService: IManageBankAccounts
    {
        private BankRepository repository;
        public BankAccountService(BankRepository _repository)
        {
            repository = _repository;
        }

        public BankAccount Create(Guid userID)
        {
            var bankAccount = new BankAccount(userID);
            var fundingTransaction = new BankTransaction(bankAccount.ID, TransactionType.CREDIT, 100);
            repository.BankAccounts.Add(bankAccount);
            repository.BankTransactions.Add(fundingTransaction);
            return bankAccount;
        }

        public BankAccount Delete(Guid accountId)
        {
            var accountToRemove = repository.BankAccounts.Single(account => account.ID == accountId);

            repository.BankAccounts.Remove(accountToRemove);
            
            return accountToRemove;
        }

        public decimal GetBalance(Guid AccountId)
        {
            return repository.BankTransactions
                .Where(tx => tx.AccountID.Equals(AccountId))
                .Aggregate(0m, (sum, tx) =>
                {
                    if (tx.TransactionType == TransactionType.CREDIT)
                    {
                        return sum + tx.Amount;
                    }

                    if (tx.TransactionType == TransactionType.DEBIT)
                    {
                        return sum - tx.Amount;
                    }

                    return sum;
                });
        }
        
        public IEnumerable<BankAccount> ListByUser(Guid userId)
        {
            return repository.BankAccounts.Where(account => account.UserID == userId);
        }
    }
}