using System;

namespace Kata.Banking.Web.Parameters
{
    public class TransactionParameter
    {
        public decimal Amount { get; set; }
        public Guid AccountId { get; set; }
    }
}