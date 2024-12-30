using Application.Services.Interfaces;
using Ecommerce.Domain.ServiceModel.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {


        [HttpPost("Add")]
        [Authorize(policy: "Admins")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequestDTO categoryRequest)
        {
            return Ok(await categoryService.Add(categoryRequest));
        }
        [HttpPut("Update/{id}")]
        [Authorize(policy: "Admins")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequestDTO categoryRequest)
        {
            var result = await categoryService.Update(categoryRequest, id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return Ok(await categoryService.GetById(id));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategory()
        {
            return Ok(await categoryService.GetAll());
        }
        [HttpDelete("Delete/{id}")]
        [Authorize(policy: "Admins")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return Ok(await categoryService.Delete(id));
        }
    }
}
