

using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.AddRoleRequest;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.DeleteRole;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.GetRoleById;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.ManageUserClaims;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.ManageUserRoles;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.UpdateRole;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.UpdateUserClaims;
using Ecommerce.Application.DTO.RequestsDTO.Authorization.UpdateUserRoles;
using Ecommerce.Application.DTO.ResponsesDTO.Authorization;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<Response<string>> AddRole(AddRoleRequest addRoleRequest);
        public Task<Response<string>> UpdateRole(UpdateRoleRequest updateRoleRequest);
        public Task<Response<string>> DeleteRole(DeleteRoleRequest deleteRoleRequest);
        public Task<Response<RoleResponse>> GetById(GetByIdRoleRequest getByIdRolesRequest);
        public Task<Response<List<RoleResponse>>> ListRoles();
        public Task<Response<ManageUserRolesResponse>> ManageUserRoles(ManageUserRolesRequest manageUserRolesRequest);
        public Task<Response<string>> UpdateUserRoles(UpdateUserRolesRequest updateUserRolesRequest);
        public Task<Response<ManageUserClaimsResponse>> ManageUserClaims(ManageUserClaimsRequest manageUserClaimsRequest);
        public Task<Response<string>> UpdateUserClaims(UpdateUserClaimsRequest updateUserClaimsRequest);

    }
}
