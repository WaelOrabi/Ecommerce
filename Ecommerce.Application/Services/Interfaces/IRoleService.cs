using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<RoleResponse> GetById(int id);
        Task<IEnumerable<RoleResponse>> GetAll();
        Task<RoleResponse> Add(RoleRequestDTO roleRequest);
        Task<RoleResponse?> Update(RoleRequestDTO roleRequest, int id);
        Task<int> Delete(int id);
    }
}
