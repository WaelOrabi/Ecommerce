using Application.Services.Interfaces;
using Ecommerce.Domain.ServiceModel;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService; 
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await  _accountService.GetById(id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _accountService.GetAll());
        }
        [HttpPost("Add")]
        public async Task<IActionResult>Add(AccountRequest accountRequest)
        {
            return Ok(await _accountService.Add(accountRequest));
        }
        [HttpPost("Update")]
        public async Task<ActionResult>Update(AccountRequest accountRequest)
        {
            return Ok(await _accountService.Update(accountRequest));
        }
        [HttpPost("Auth")]
        public async Task<IActionResult> Auth(AuthAccount authAccount)
        {
            var authResult = await _accountService.Auth(authAccount);
            if (authResult == "Account not found") 
            {
                return Unauthorized();
            }
            return Ok(authResult);
        }
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>Delete(int id)
        {
            return Ok(await _accountService.Delete(id));
        }

    }
}
