using System;
using System.Collections.Generic;
using System.ComponentModel;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Kata.Banking.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IManageUsers _manageUsersService;
        private readonly IManageBankAccounts _bankAccountService;
        
        public UserController(IManageUsers manageUsersService, IManageBankAccounts bankAccountService)
        {
            _manageUsersService = manageUsersService;
            _bankAccountService = bankAccountService;
        }
        
        [HttpGet("{userId}")]
        [SwaggerOperation(Summary="Get a list of bank accounts belonging to a specific user.")]
        [ProducesResponseType(typeof(List<BankAccount>), StatusCodes.Status200OK)]
        public IActionResult GetBankAccountsForUser([FromRoute] Guid userId)
        {
            var accounts = _bankAccountService.ListByUser(userId);
            return Ok(accounts);
        }

        [HttpPost]
        [SwaggerOperation(Summary="Create a new user.")]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        public IActionResult CreateUser()
        {
            var user = _manageUsersService.Create();
            return Created("", user);
        }
    }
}