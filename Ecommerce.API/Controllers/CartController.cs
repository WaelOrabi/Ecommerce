using Ecommerce.API.Base;
using Ecommerce.Application.DTO.RequestsDTO.Cart;
using Ecommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{

    [ApiController]
    [Authorize]
    public class CartController(ICartService cartService) : AppControllerBase
    {
        [HttpGet(Router.CartRouting.GetById)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await cartService.GetById(id);
            return NewResult(result);
        }
        [HttpGet(Router.CartRouting.List)]
        public async Task<IActionResult> GetAll()
        {
            var result = await cartService.GetAll();
            return NewResult(result);
        }
        [HttpPost(Router.CartRouting.Create)]
        public async Task<IActionResult> AddCart(CartRequest cartRequest)
        {
            var result = await cartService.Add(cartRequest);
            return NewResult(result);
        }
        [HttpPut(Router.CartRouting.Update)]
        public async Task<IActionResult> UpdateCart(int id, CartRequest cartRequest)
        {

            var order = await cartService.Update(cartRequest, id);

            return NewResult(order);

        }
        [HttpDelete(Router.CartRouting.Delete)]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var result = await cartService.Delete(id);
            return NewResult(result);
        }
    }
}
