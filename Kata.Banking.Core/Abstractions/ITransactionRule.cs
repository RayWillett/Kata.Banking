using Kata.Banking.Data;

namespace Kata.Banking.Core.Abstractions
{
    public interface ITransactionRule
    {
        bool Test(decimal currentBankBalance, BankTransaction candidateTransaction);
    }
}