using Application.Services.Interfaces;
using Ecommerce.API.Base;
using Ecommerce.Application.DTO.RequestsDTO.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{

    [ApiController]
    [Authorize]
    public class AddressController : AppControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet(Router.AddressRouting.GetById)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _addressService.GetById(id);
            return NewResult(result);
        }
        [HttpGet(Router.AddressRouting.List)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _addressService.GetAll();
            return NewResult(result);

        }
        [HttpPost(Router.AddressRouting.Create)]
        [Authorize(policy: "Admins")]
        public async Task<IActionResult> AddAddress(AddressRequest addressRequest)
        {
            var result = await _addressService.Add(addressRequest);
            return NewResult(result);
        }
        [HttpPut(Router.AddressRouting.Update)]
        [Authorize(policy: "Admins")]
        public async Task<IActionResult> UpdateAddress(int id, AddressRequest addressRequest)
        {
            var result = await _addressService.Update(addressRequest, id);

            return NewResult(result);
        }
        [HttpDelete(Router.AddressRouting.Delete)]
        [Authorize(policy: "Admins")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var result = await _addressService.Delete(id);
            return NewResult(result);
        }
    }
}
