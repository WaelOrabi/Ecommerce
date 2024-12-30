using Application.Services.Interfaces;
using Ecommerce.Domain.ServiceModel.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await orderService.GetById(id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await orderService.GetAll());
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddOrder(OrderRequestDTO orderRequest)
        {
            try
            {
                var order = await orderService.Add(orderRequest);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderRequestDTO orderRequest)
        {
            try
            {
                var order = await orderService.Update(orderRequest, id);
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
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return Ok(await orderService.Delete(id));
        }
    }
}
