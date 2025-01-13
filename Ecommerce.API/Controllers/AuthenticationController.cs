using Ecommerce.API.Base;
using Ecommerce.Application.DTO.RequestsDTO.Authentication.GetRefreshToken;
using Ecommerce.Application.DTO.RequestsDTO.Authentication.Sigin;
using Ecommerce.Application.DTO.RequestsDTO.Authentication.ValidateToken;
using Ecommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{

    [ApiController]
    [Authorize()]
    public class AuthenticationController : AppControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost(Router.AuthenticationRouting.SigIn)]
        [AllowAnonymous()]
        public async Task<IActionResult> SigIn([FromForm] SigInRequest sigInRequest)
        {
            var result = await _authenticationService.SigIn(sigInRequest);
            return NewResult(result);
        }
        [HttpPost(Router.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenRequest refreshTokenRequest)
        {
            var result = await _authenticationService.GetRefreshToken(refreshTokenRequest);
            return NewResult(result);
        }
        [HttpGet(Router.AuthenticationRouting.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] ValidateTokenRequest validateTokenRequest)
        {
            var result = await _authenticationService.ValidateToken(validateTokenRequest);
            return NewResult(result);
        }
    }
}
