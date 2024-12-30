using Application.Services.Interfaces;
using Ecommerce.Domain.ServiceModel.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(policy: "Admins")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("Get/{id}")]

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _roleService.GetById(id));
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _roleService.GetAll());
        }
        [HttpPost("Add")]

        public async Task<IActionResult> AddRole(RoleRequestDTO roleRequest)
        {
            return Ok(await _roleService.Add(roleRequest));
        }
        [HttpPut("Update/{id}")]

        public async Task<IActionResult> UpdateRole([FromRoute] int id, RoleRequestDTO roleRequest)
        {
            var role = await _roleService.Update(roleRequest, id);
            if (role == null)
                return NotFound();
            return Ok(role);
        }
        [HttpDelete("Delete/{id}")]

        public async Task<ActionResult> DeleteRole(int id)
        {
            return Ok(await _roleService.Delete(id));
        }
    }
}
