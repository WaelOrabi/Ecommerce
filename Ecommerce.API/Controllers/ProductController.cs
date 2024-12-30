using Application.Services.Interfaces;
using Ecommerce.Domain.ServiceModel.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productService.GetById(id));
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAll());
        }
        [HttpPost("Add")]
        [Authorize(policy: "Admins")]
        public async Task<IActionResult> AddProduct([FromForm] ProductRequestDTO productRequest)
        {

            return Ok(await _productService.Add(productRequest));

        }
        [HttpPut("Update/{id}")]
        [Authorize(policy: "Admins")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromForm] ProductRequestDTO productRequest)
        {
            var result = await _productService.Update(productRequest, id);
            if (result == null)
                return NotFound();
            return Ok(result);

        }
        [HttpDelete("Delete/{id}")]
        [Authorize(policy: "Admins")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            return Ok(await _productService.Delete(id));

        }
    }
}
