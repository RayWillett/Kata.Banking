using System;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Data;
using Kata.Banking.Web.OutputParameters;
using Kata.Banking.Web.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kata.Banking.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController: ControllerBase
    {
        private readonly IManageBankAccounts _manageBankService;
        
        public BankAccountController(IManageBankAccounts manageBankService)
        {
            _manageBankService = manageBankService;
        }
        
        [HttpPost]
        [SwaggerOperation(Summary="Create a new bank account for a user. It is automatically funded with $100")]
        [ProducesResponseType(typeof(BankAccount), StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] IdInputParameter user)
        {
            var bankAccount = _manageBankService.Create(user.ID);
            return Created("", bankAccount);
        }

        [HttpGet("{accountId}")]
        [SwaggerOperation(Summary="Get the balance for a specific bank account.")]
        [ProducesResponseType(typeof(BankBalance), StatusCodes.Status200OK)]
        public IActionResult GetAccountBalance([FromRoute] Guid accountId)
        {
            var bankBalance = _manageBankService.GetBalance(accountId);
            return Ok(new BankBalance
            {
                Amount = bankBalance
            });
        }
        
        [HttpDelete("{accountId}")]
        [SwaggerOperation(Summary="Delete a specific bank account. Associated transactions are not deleted.")]
        [ProducesResponseType(typeof(BankAccount), StatusCodes.Status200OK)]
        public IActionResult DeleteBankAccount([FromRoute] Guid accountId)
        {
            var bankAccount = _manageBankService.Delete(accountId);
            return Ok(bankAccount);
        }
    }
}