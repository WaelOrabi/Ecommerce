using Application.Services.Interfaces;
using Ecommerce.API.Base;
using Ecommerce.Application.DTO.RequestsDTO.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{

    [ApiController]
    [Authorize(policy: "Admins")]
    public class RoleController : AppControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet(Router.RoleRouting.GetById)]

        public async Task<IActionResult> GetById(int id)
        {
            var result = await _roleService.GetById(id);
            return NewResult(result);
        }
        [HttpGet(Router.RoleRouting.List)]

        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.GetAll();
            return NewResult(result);
        }
        [HttpPost(Router.RoleRouting.Create)]

        public async Task<IActionResult> AddRole(RoleRequest roleRequest)
        {
            var result = await _roleService.Add(roleRequest);
            return NewResult(result);
        }
        [HttpPut(Router.RoleRouting.Update)]

        public async Task<IActionResult> UpdateRole([FromRoute] int id, RoleRequest roleRequest)
        {
            var role = await _roleService.Update(roleRequest, id);

            return NewResult(role);
        }
        [HttpDelete(Router.RoleRouting.Delete)]

        public async Task<ActionResult> DeleteRole(int id)
        {
            var result = await _roleService.Delete(id);
            return NewResult(result);
        }
    }
}
