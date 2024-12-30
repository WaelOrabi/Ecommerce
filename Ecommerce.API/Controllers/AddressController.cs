using Application.Services.Interfaces;
using Ecommerce.Domain.ServiceModel.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _addressService.GetById(id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _addressService.GetAll());

        }
        [HttpPost("Add")]
        [Authorize(policy: "Admins")]
        public async Task<IActionResult> AddAddress(AddressRequestDTO addressRequest)
        {
            return Ok(await _addressService.Add(addressRequest));
        }
        [HttpPut("Update/{id}")]
        [Authorize(policy: "Admins")]
        public async Task<IActionResult> UpdateAddress(int id, AddressRequestDTO addressRequest)
        {
            var result = await _addressService.Update(addressRequest, id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpDelete("Delete/{id}")]
        [Authorize(policy: "Admins")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            return Ok(await _addressService.Delete(id));
        }
    }
}
