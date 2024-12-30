using Application.Services.Interfaces;
using Ecommerce.Domain.ServiceModel.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("GetById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _accountService.GetById(id));
        }
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _accountService.GetAll());
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(AccountRequest accountRequest)
        {
            return Ok(await _accountService.Add(accountRequest));
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<ActionResult> Update(int id, AccountRequest accountRequest)
        {
            var result = await _accountService.Update(accountRequest, id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPost("Auth")]
        public async Task<IActionResult> Auth(AuthAccountRequestDTO authAccount)
        {
            var authResult = await _accountService.Auth(authAccount);
            if (authResult == "Account not found")
            {
                return Unauthorized();
            }
            return Ok(authResult);
        }
        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _accountService.Delete(id));
        }

    }
}
