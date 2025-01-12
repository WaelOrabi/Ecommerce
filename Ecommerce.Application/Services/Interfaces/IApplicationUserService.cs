
using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.ChangeUserPassword;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.DeleteUser;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.EditUser;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.GetListUsersPaginate;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.GetUserById;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.RegisterUser;
using Ecommerce.Application.DTO.ResponsesDTO;
using Ecommerce.Domain.DTO.Responses;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface IApplicationUserService
    {
        public Task<Response<string>> RegisterUser(UserRegisterRequest userRegisterRequest);
        public Task<Response<string>> DeleteUser(DeleteUserRequest deleteUser);
        public Task<Response<string>> ChangeUserPassword(ChangeUserPasswordRequest changeUserPasswordRequest);
        public Task<Response<string>> EditUser(EditUserRequest editUserRequest);
        public Task<Response<UserResponse>> GetUserById(GetUserByIdRequest getUserByIdRequest);
        public Task<Response<List<ListUserResponse>>> GetAllUsers();
        public Task<PaginatedResponse<ListUserPaginateResponse>> GetUsersPaginate(GetListUsersPaginateRequest getListUsersPaginateRequest);
    }
}
