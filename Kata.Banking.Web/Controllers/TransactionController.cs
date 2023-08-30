using Kata.Banking.Core.Abstractions;
using Kata.Banking.Data;
using Kata.Banking.Web.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kata.Banking.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController: ControllerBase
    {
        private readonly ICreateTransactions _createTransactionsService;
        
        public TransactionController(ICreateTransactions createTransactionsService)
        {
            _createTransactionsService = createTransactionsService;
        }

        [HttpPost("credit")]
        [SwaggerOperation(Summary="Create a transaction that will increase the balance of a bank account.")]
        [ProducesResponseType(typeof(BankTransaction), StatusCodes.Status200OK)]
        public IActionResult PostCredit([FromBody] TransactionParameter transactionParameter)
        {
            var tx = _createTransactionsService.CreditAccount(transactionParameter.AccountId, transactionParameter.Amount);
            return Ok(tx);
        }
        
        [HttpPost("debit")]
        [SwaggerOperation(Summary="Create a transaction that will decrease the balance of a bank account.")]
        [ProducesResponseType(typeof(BankTransaction), StatusCodes.Status200OK)]
        public IActionResult PostDebit([FromBody] TransactionParameter transactionParameter)
        {
            var tx = _createTransactionsService.DebitAccount(transactionParameter.AccountId, transactionParameter.Amount);
            return Ok(tx);
        }
    }
}