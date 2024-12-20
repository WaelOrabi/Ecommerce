using Application.Interfaces;
using Application.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("Get/{id}")]
        public async  Task<IActionResult> GetById(int id) { 
        return Ok(await _productService.GetById(id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAll());
        }
        [HttpPost("Add")]
        public async Task<IActionResult>AddProduct([FromForm]ProductRequest productRequest)
        {

            return Ok(await _productService.Add(productRequest));

        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductRequest productRequest)
        {

            return Ok(await _productService.Add(productRequest));

        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> UpdateProduct(int id)
        {

            return Ok(await _productService.Delete(id));

        }
    }
}
