using System;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Data;

namespace Kata.Banking.Core.Service
{
    public class TransactionService: ICreateTransactions
    {
        private BankRepository repository;
        private IManageBankAccounts _manageBankService;
        private ITransactionRuleEngine rules;
        public TransactionService(BankRepository _repository, IManageBankAccounts manageBankService, ITransactionRuleEngine _rules)
        {
            repository = _repository;
            _manageBankService = manageBankService;
            rules = _rules;
        }

        public BankTransaction DebitAccount(Guid accountId, decimal amount)
        {
            var bankTransaction = new BankTransaction(accountId, TransactionType.DEBIT, amount);
            if (rules.ApplyRules(_manageBankService.GetBalance(accountId), bankTransaction))
            {
                repository.BankTransactions.Add(bankTransaction);
            }
            return bankTransaction;
        }

        public BankTransaction CreditAccount(Guid accountId, decimal amount)
        {
            var bankTransaction = new BankTransaction(accountId, TransactionType.CREDIT, amount);
            if (rules.ApplyRules(_manageBankService.GetBalance(accountId), bankTransaction))
            {
                repository.BankTransactions.Add(bankTransaction);
            }
            return bankTransaction;
        }
    }
}