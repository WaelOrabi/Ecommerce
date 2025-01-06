using Application.Services.Interfaces;
using Ecommerce.API.Base;
using Ecommerce.Application.DTO.RequestsDTO.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{

    [ApiController]
    [Authorize]
    public class OrderController(IOrderService orderService) : AppControllerBase
    {
        [HttpGet(Router.OrderRouting.GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await orderService.GetById(id);
            return NewResult(result);
        }
        [HttpGet(Router.OrderRouting.List)]
        public async Task<IActionResult> GetAll()
        {
            var result = await orderService.GetAll();
            return NewResult(result);
        }

        [HttpPost(Router.OrderRouting.Create)]
        public async Task<IActionResult> AddOrder(OrderRequest orderRequest)
        {

            var order = await orderService.Add(orderRequest);
            return NewResult(order);

        }
        [HttpPut(Router.OrderRouting.Update)]
        public async Task<IActionResult> UpdateOrder(int id, OrderRequest orderRequest)
        {

            var order = await orderService.Update(orderRequest, id);

            return NewResult(order);

        }
        [HttpDelete(Router.OrderRouting.Delete)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await orderService.Delete(id);
            return NewResult(result);
        }
    }
}
