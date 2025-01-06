using Application.Services.Interfaces;
using Ecommerce.API.Base;
using Ecommerce.Application.DTO.RequestsDTO.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{

    [ApiController]
    // [Authorize]
    public class ProductController : AppControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet(Router.ProductRouting.GetById)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetById(id);
            return NewResult(result);
        }
        [HttpGet(Router.ProductRouting.List)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAll();
            return NewResult(result);
        }

        // [Authorize(policy: "Admins")]
        [HttpPost(Router.ProductRouting.Create)]
        public async Task<IActionResult> AddProduct([FromForm] ProductRequest productRequest)
        {



            var result = await _productService.Add(productRequest);
            return NewResult(result);
        }

        [HttpPut(Router.ProductRouting.Update)]
        //  [Authorize(policy: "Admins")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromForm] ProductRequest productRequest)
        {
            var result = await _productService.Update(productRequest, id);

            return NewResult(result);

        }
        [HttpDelete(Router.ProductRouting.Delete)]
        //[Authorize(policy: "Admins")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.Delete(id);
            return NewResult(result);

        }
    }
}
