using Kata.Banking.Data;

namespace Kata.Banking.Core.Abstractions
{
    public interface ITransactionRuleEngine
    {
        bool ApplyRules(decimal currentBankBalance, BankTransaction candidateTransaction);
    }
}