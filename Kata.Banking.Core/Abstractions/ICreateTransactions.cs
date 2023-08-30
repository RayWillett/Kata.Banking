using System;
using Kata.Banking.Data;

namespace Kata.Banking.Core.Abstractions
{
    public interface ICreateTransactions
    {
        BankTransaction DebitAccount(Guid accountId, decimal amount);
        BankTransaction CreditAccount(Guid accountId, decimal amount);
    }
}