using Ecommerce.API.Base;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.DeleteUser;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.EditUser;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.GetListUsersPaginate;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.GetUserById;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.RegisterUser;
using Ecommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{

    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {
        private readonly IApplicationUserService _applicationUserService;
        public ApplicationUserController(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }
        [HttpPost(Router.ApplicationUserRouting.Create)]
        public async Task<IActionResult> Add(UserRegisterRequest userRegisterRequest)
        {
            var result = await _applicationUserService.RegisterUser(userRegisterRequest);
            return NewResult(result);
        }
        [HttpPut(Router.ApplicationUserRouting.Update)]
        //  [Authorize]

        public async Task<ActionResult> Update(EditUserRequest editUserRequest)
        {
            var result = await _applicationUserService.EditUser(editUserRequest);

            return NewResult(result);
        }
        [HttpDelete(Router.ApplicationUserRouting.Delete)]
        //  [Authorize(Policy = "Admins")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _applicationUserService.DeleteUser(new DeleteUserRequest() { Id = id });
            return NewResult(result);
        }
        [HttpGet(Router.ApplicationUserRouting.GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _applicationUserService.GetUserById(new GetUserByIdRequest() { Id = id });
            return NewResult(result);
        }
        [HttpGet(Router.ApplicationUserRouting.List)]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _applicationUserService.GetAllUsers();
            return NewResult(result);
        }
        [HttpGet(Router.ApplicationUserRouting.ListPagination)]
        public async Task<IActionResult> GetListPaginationUsers([FromQuery] GetListUsersPaginateRequest request)
        {
            var result = await _applicationUserService.GetUsersPaginate(request);
            return Ok(result);
        }
    }
}
