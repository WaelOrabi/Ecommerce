using Application.Interfaces;
using Application.Services.Interfaces;
using Ecommerce.Application.Authorization;
using Ecommerce.Domain;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("Get/{id}")]
        [Authorize(Policy = "AgeGreaterThan25")]
        public async  Task<IActionResult> GetById(int id) { 
        return Ok(await _productService.GetById(id));
        }
        [HttpGet("GetAll")]
        //  [CheckPermission(Permission.Read)]
        //[Authorize(Roles = "Admin,SuperUser")]// role : "admin" or "superuser"
        //[Authorize(Roles ="SuperUser")]//role: "admin" and "superuser"
        [Authorize(Policy ="SuperUsersOnly")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAll());
        }
        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>AddProduct([FromForm]ProductRequest productRequest)
        {

            return Ok(await _productService.Add(productRequest));

        }
        [HttpPost("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductRequest productRequest)
        {

            return Ok(await _productService.Add(productRequest));

        }
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id)
        {

            return Ok(await _productService.Delete(id));

        }
    }
}
