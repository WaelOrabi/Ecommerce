using Ecommerce.API.Base;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.AddRoleRequest;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.DeleteRole;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.GetRoleById;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.ManageUserClaims;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.ManageUserRoles;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.UpdateRole;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.UpdateUserClaims;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.UpdateUserRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = Ecommerce.Application.Services.Interfaces.IAuthorizationService;

namespace Ecommerce.API.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [HttpPost(Router.AuthorizationRouting.Create)]
        public async Task<IActionResult> CreateRole([FromForm] AddRoleRequest addRoleRequest)
        {
            var result = await _authorizationService.AddRole(addRoleRequest);
            return NewResult(result);
        }
        [HttpPut(Router.AuthorizationRouting.Update)]
        public async Task<IActionResult> UpdateRole([FromForm] UpdateRoleRequest updateRoleRequest)
        {
            var result = await _authorizationService.UpdateRole(updateRoleRequest);
            return NewResult(result);
        }
        [HttpDelete(Router.AuthorizationRouting.Delete)]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            var result = await _authorizationService.DeleteRole(new DeleteRoleRequest() { Id = id });
            return NewResult(result);
        }
        [HttpGet(Router.AuthorizationRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _authorizationService.GetById(new GetByIdRoleRequest() { Id = id });
            return NewResult(result);
        }
        [HttpGet(Router.AuthorizationRouting.GetRoles)]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _authorizationService.ListRoles();
            return NewResult(result);
        }
        [HttpGet(Router.AuthorizationRouting.ManageUserRoles)]
        public async Task<IActionResult> ManageUserRoles([FromRoute] int userId)
        {
            var response = await _authorizationService.ManageUserRoles(new ManageUserRolesRequest() { UserId = userId });
            return NewResult(response);
        }
        [HttpPut(Router.AuthorizationRouting.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesRequest updateUserRolesRequest)
        {
            var response = await _authorizationService.UpdateUserRoles(updateUserRolesRequest);
            return NewResult(response);
        }
        [HttpGet(Router.AuthorizationRouting.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaims([FromRoute] int userId)
        {
            var response = await _authorizationService.ManageUserClaims(new ManageUserClaimsRequest() { UserId = userId });
            return NewResult(response);
        }
        [HttpPut(Router.AuthorizationRouting.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsRequest updateUserClaimsRequest)
        {
            var response = await _authorizationService.UpdateUserClaims(updateUserClaimsRequest);
            return NewResult(response);
        }
    }
}
