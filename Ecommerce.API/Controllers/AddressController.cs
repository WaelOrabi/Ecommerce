using Application.Services.Interfaces;
using Ecommerce.Domain.ServiceModel;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;   
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id) {
            return Ok(await _addressService.GetById(id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult>GetAll(int id)
        {
            return Ok(await _addressService.GetAll());

        }
        [HttpPost("Add")]
        public async Task<IActionResult>AddAddress(AddressRequest addressRequest)
        {
            return Ok(await _addressService.Add(addressRequest));
        }
        [HttpPost("Update")]
        public async Task<IActionResult>UpdateAddress(AddressRequest addressRequest)
        {
            return Ok(await _addressService.Update(addressRequest));
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            return Ok(await _addressService.Delete(id));
        }
    }
}
