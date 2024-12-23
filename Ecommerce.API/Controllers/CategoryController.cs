using Application.Interfaces;
using Application.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
   

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory([FromBody]CategoryRequest categoryRequest)
        {
            return Ok(await categoryService.Add(categoryRequest));
        }
        [HttpPost("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryRequest categoryRequest)
        {
            return Ok(await categoryService.Update(categoryRequest));
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return Ok(await categoryService.Delete(id));
        }
    }
}
