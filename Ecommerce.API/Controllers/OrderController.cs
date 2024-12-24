using Application.Services.Interfaces;
using Ecommerce.Application.Services;
using Ecommerce.Domain.ServiceModel.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpGet("Get/{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            return Ok(await orderService.GetById(id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await orderService.GetAll());
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddOrder(OrderRequest orderRequest) {
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
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateOrder(OrderRequest orderRequest)
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
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult>DeleteOrder(int id)
        {
            return Ok(await orderService.Delete(id));
        }
    }
}
