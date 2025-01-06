using Application.Services.Interfaces;
using Ecommerce.API.Base;
using Ecommerce.Application.DTO.RequestsDTO.AuthAccount;
using Ecommerce.Domain.DTO.RequestsDTO.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Ecommerce.API.Controllers
{

    [ApiController]
    //[ResponseCache(CacheProfileName = "120SecondsDuration")]
    [OutputCache(PolicyName = "120SecondsDuration")]
    public class AccountController : AppControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet(Router.AccountRouting.GetById)]
        // [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _accountService.GetById(id);
            return NewResult(response);
        }
        [HttpGet(Router.AccountRouting.List)]
        //  [ResponseCache(Duration = 60)]
        [OutputCache(Duration = 60)]

        //  [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _accountService.GetAll();
            return NewResult(result);
        }
        [HttpPost(Router.AccountRouting.Create)]

        public async Task<IActionResult> Add(AccountRequest accountRequest)
        {
            var result = await _accountService.Add(accountRequest);
            return NewResult(result);
        }
        [HttpPut(Router.AccountRouting.Update)]
        [Authorize]

        public async Task<ActionResult> Update(int id, AccountRequest accountRequest)
        {
            var result = await _accountService.Update(accountRequest, id);

            return NewResult(result);
        }
        [HttpPost(Router.AccountRouting.Auth)]
        public async Task<IActionResult> Auth(AuthAccountRequest authAccount)
        {
            var authResult = await _accountService.Auth(authAccount);

            return NewResult(authResult);
        }
        [HttpDelete(Router.AccountRouting.Delete)]
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _accountService.Delete(id);
            return NewResult(result);
        }

    }
}
