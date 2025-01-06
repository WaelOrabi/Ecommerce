using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Role;
using Ecommerce.Domain.DTO.ResponsesDTO;

namespace Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Response<RoleResponse>> GetById(int id);
        Task<Response<IEnumerable<RoleResponse>>> GetAll();
        Task<bool> IsRoleExist(int id);
        Task<Response<string>> Add(RoleRequest roleRequest);
        Task<Response<string>> Update(RoleRequest roleRequest, int id);
        Task<Response<int>> Delete(int id);
    }
}
