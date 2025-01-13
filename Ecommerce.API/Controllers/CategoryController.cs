using Application.Services.Interfaces;
using Ecommerce.API.Base;
using Ecommerce.Application.DTO.RequestsDTO.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController(ICategoryService categoryService) : AppControllerBase
    {


        [HttpPost(Router.CategoryRouting.Create)]

        public async Task<IActionResult> AddCategory([FromBody] CategoryRequest categoryRequest)
        {
            var result = await categoryService.Add(categoryRequest);
            return NewResult(result);
        }
        [HttpPut(Router.CategoryRouting.Update)]

        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest categoryRequest)
        {
            var result = await categoryService.Update(categoryRequest, id);

            return NewResult(result);
        }
        [HttpGet(Router.CategoryRouting.GetById)]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategory(int id)
        {
            var result = await categoryService.GetById(id);
            return NewResult(result);
        }

        [HttpGet(Router.CategoryRouting.List)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await categoryService.GetAll();
            return NewResult(result);
        }
        [HttpDelete(Router.CategoryRouting.Delete)]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await categoryService.Delete(id);
            return NewResult(result);
        }
    }
}
