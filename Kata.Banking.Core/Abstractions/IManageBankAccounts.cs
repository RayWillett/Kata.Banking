using System;
using System.Collections;
using System.Collections.Generic;
using Kata.Banking.Data;

namespace Kata.Banking.Core.Abstractions
{
    public interface IManageBankAccounts
    {
        BankAccount Create(Guid userID);
        BankAccount Delete(Guid accountId);
        IEnumerable<BankAccount> ListByUser(Guid userId);
        decimal GetBalance(Guid accountId);
    }
}