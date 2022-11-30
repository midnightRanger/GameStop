using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStop.DAL.Interface;
using GameStop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Controllers
{
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _IAccount;

        public AccountController(IAccount iAccount)
        {
            _IAccount = iAccount; 
        }
        
        // GET: api/account>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountModel>>> Get()
        {
            return await Task.FromResult(_IAccount.getAccounts());
        }
        
        // GET: api/account/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountModel>> Get(int id)
        {
            var account = await Task.FromResult(_IAccount.getAccount(id));
            if (account == null)
            {
                return NotFound();
            }
            return account;
        }
        
        // POST api/account
        [HttpPost]
        public async Task<ActionResult<AccountModel>> Post(AccountModel account)
        {
            _IAccount.addAccount(account);
            return await Task.FromResult(account);
        }
        
        // PUT api/account/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AccountModel>> Put(int id, AccountModel account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }
            try
            {
                _IAccount.updateAccount(account);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(account);
        }
        
        // DELETE api/account/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountModel>> Delete(int id)
        {
            var account = _IAccount.deleteAccount(id);
            return await Task.FromResult(account); 
        }
        private bool AccountExist(int id) => _IAccount.checkAccount(id);


    }
}
