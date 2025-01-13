using AutoMapper;
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
using Ecommerce.Application.Resources;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.Entities.Identity;
using Ecommerce.Domain.Helpers;
using Ecommerce.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace Ecommerce.Application.Services.Implementation
{
    public class AuthorizationService : ResponseHandler, IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;
        public AuthorizationService(RoleManager<Role> roleManager, IMapper mapper, IStringLocalizer<SharedResources> localizer, UserManager<User> userManager, ApplicationDbContext dbContext) : base(localizer)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _localizer = localizer;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        public async Task<Response<string>> AddRole(AddRoleRequest addRoleRequest)
        {
            var role = _mapper.Map<Role>(addRoleRequest);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded) return GenerateSuccessResponse("");
            var errors = string.Join("-", result.Errors);
            return GenerateBadRequestResponse<string>(errors);
        }

        public async Task<Response<string>> DeleteRole(DeleteRoleRequest deleteRoleRequest)
        {
            var role = await _roleManager.FindByIdAsync(deleteRoleRequest.Id.ToString());
            if (role == null)
                return GenerateNotFoundResponse<string>(_localizer[SharedResourcesKeys.NotFound]);
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users != null && users.Count() > 0) return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.RoleUsed]);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) return GenerateSuccessResponse<string>("");
            var errors = string.Join("-", result.Errors);
            return GenerateBadRequestResponse<string>(errors);
        }

        public async Task<Response<RoleResponse>> GetById(GetByIdRoleRequest getByIdRolesRequest)
        {
            var role = await _roleManager.FindByIdAsync(getByIdRolesRequest.Id.ToString());
            if (role == null)
                return GenerateNotFoundResponse<RoleResponse>(_localizer[SharedResourcesKeys.RoleNotFound]);
            var result = _mapper.Map<RoleResponse>(role);
            return GenerateSuccessResponse(result);
        }

        public async Task<Response<List<RoleResponse>>> ListRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var result = _mapper.Map<List<RoleResponse>>(roles);
            return GenerateSuccessResponse(result);
        }

        public async Task<Response<ManageUserRolesResponse>> ManageUserRoles(ManageUserRolesRequest manageUserRolesRequest)
        {

            var user = await _userManager.FindByIdAsync(manageUserRolesRequest.UserId.ToString());
            if (user == null) return GenerateNotFoundResponse<ManageUserRolesResponse>(_localizer[SharedResourcesKeys.UserNotFound]);
            var roles = await _roleManager.Roles.ToListAsync();
            var response = new ManageUserRolesResponse();
            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var roleUserResponse = new RoleUserResponse()
                {
                    Id = role.Id,
                    Name = role.Name,
                    HasRole = await _userManager.IsInRoleAsync(user, role.Name)
                };
                response.Roles.Add(roleUserResponse);

            }
            return GenerateSuccessResponse(response);
        }
        public async Task<Response<string>> UpdateUserRoles(UpdateUserRolesRequest updateUserRolesRequest)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(updateUserRolesRequest.UserId.ToString());
                if (user == null)
                    return GenerateNotFoundResponse<string>(_localizer[SharedResourcesKeys.UserNotFound]);
                var userRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                    return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
                var selectedRoles = updateUserRolesRequest.Roles.Where(x => x.HasRole == true).Select(x => x.Name);
                var addRolesResult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addRolesResult.Succeeded)
                    return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.FailedToAddNewRoles]);
                await transact.CommitAsync();
                return GenerateSuccessResponse("");

            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
            }
        }
        public async Task<Response<string>> UpdateRole(UpdateRoleRequest updateRoleRequest)
        {
            var role = await _roleManager.FindByIdAsync(updateRoleRequest.Id.ToString());
            if (role == null)
                return GenerateNotFoundResponse<string>(_localizer[SharedResourcesKeys.NotFound]);
            var newRole = _mapper.Map(updateRoleRequest, role);
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return GenerateSuccessResponse("");
            var errors = string.Join("-", result.Errors);
            return GenerateBadRequestResponse<string>(errors);
        }

        public async Task<Response<ManageUserClaimsResponse>> ManageUserClaims(ManageUserClaimsRequest manageUserClaimsRequest)
        {
            var user = await _userManager.FindByIdAsync(manageUserClaimsRequest.UserId.ToString());
            if (user == null) return GenerateNotFoundResponse<ManageUserClaimsResponse>(_localizer[SharedResourcesKeys.UserNotFound]);
            var response = new ManageUserClaimsResponse();
            response.UserId = user.Id;
            var userClaimsList = await _userManager.GetClaimsAsync(user);
            foreach (var item in ClaimsStroe.claims)
            {
                if (userClaimsList.Any(x => x.Type == item.Type))
                    response.userClaims.Add(new UserClaimsResponse()
                    {
                        Type = item.Type,
                        Value = true
                    });
                else
                    response.userClaims.Add(new UserClaimsResponse()
                    {
                        Type = item.Type,
                        Value = false
                    });
            }
            return GenerateSuccessResponse(response);
        }



        public async Task<Response<string>> UpdateUserClaims(UpdateUserClaimsRequest updateUserClaimsRequest)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(updateUserClaimsRequest.UserId.ToString());
                if (user == null)
                    return GenerateNotFoundResponse<string>(_localizer[SharedResourcesKeys.UserNotFound]);
                var userClaims = await _userManager.GetClaimsAsync(user);

                var removeResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeResult.Succeeded)
                    return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.FailedToRemoveOldClaims]);

                var selectedClaims = updateUserClaimsRequest.userClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));
                var addClaimsResult = await _userManager.AddClaimsAsync(user, selectedClaims);
                if (!addClaimsResult.Succeeded)
                    return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.FailedToAddNewClaims]);

                await transact.CommitAsync();

                return GenerateSuccessResponse<string>("");
            }
            catch (Exception exception)
            {
                await transact.RollbackAsync();
                return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.FailedToUpdateUserClaims]);
            }
        }


    }
}
