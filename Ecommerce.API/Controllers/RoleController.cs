using Application.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id) { 
        return Ok(await _roleService.GetById(id));   
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _roleService.GetAll());
        }
        [HttpPost("Add")]
        public async Task<IActionResult>AddRole(RoleRequest roleRequest)
        {
            return Ok(await _roleService.Add(roleRequest));
        }
        [HttpPost("Update")]
        public async Task<IActionResult>UpdateRole(RoleRequest roleRequest)
        {
            return Ok(await _roleService.Update(roleRequest));
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult>DeleteRole(int id)
        {
            return Ok(await _roleService.Delete(id));
        }
    }
}
