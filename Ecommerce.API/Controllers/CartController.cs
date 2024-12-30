using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.ServiceModel.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await cartService.GetById(id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await cartService.GetAll());
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddCart(CartRequestDTO cartRequest)
        {
            return Ok(await cartService.Add(cartRequest));
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCart(int id, CartRequestDTO cartRequest)
        {
            try
            {
                var order = await cartService.Update(cartRequest, id);
                if (order == null)
                    return NotFound();
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            return Ok(await cartService.Delete(id));
        }
    }
}
