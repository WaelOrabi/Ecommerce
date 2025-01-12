using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.ChangeUserPassword;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.DeleteUser;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.EditUser;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.GetListUsersPaginate;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.GetUserById;
using Ecommerce.Application.DTO.RequestsDTO.ApplicationUser.RegisterUser;
using Ecommerce.Application.DTO.ResponsesDTO;
using Ecommerce.Application.Extensions;
using Ecommerce.Application.Resources;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.DTO.Responses;
using Ecommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.Services.Implementation
{
    public class ApplicationUserService : ResponseHandler, IApplicationUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public ApplicationUserService(UserManager<User> userManager, IStringLocalizer<SharedResources> localizer, IMapper mapper) : base(localizer)
        {
            _userManager = userManager;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<Response<string>> ChangeUserPassword(ChangeUserPasswordRequest changeUserPasswordRequest)
        {
            var user = await _userManager.FindByIdAsync(changeUserPasswordRequest.Id.ToString());
            if (user == null)
                return GenerateBadRequestResponse<string>();
            var result = await _userManager.ChangePasswordAsync(user, changeUserPasswordRequest.CurrentPassword, changeUserPasswordRequest.NewPassword);
            if (!result.Succeeded)
                return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.ChangePasswordFailed]);
            return GenerateSuccessResponse<string>("");
        }

        public async Task<Response<string>> DeleteUser(DeleteUserRequest deleteUser)
        {
            var user = await _userManager.FindByIdAsync(deleteUser.Id.ToString());
            if (user == null)
                return GenerateBadRequestResponse<string>();
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.DeletedFailed]);

            return GenerateDeleteResponse<string>("");


        }

        public async Task<Response<string>> EditUser(EditUserRequest editUserRequest)
        {
            var user = await _userManager.FindByIdAsync(editUserRequest.Id.ToString());
            if (user == null)
                return GenerateBadRequestResponse<string>();
            var newUser = _mapper.Map(editUserRequest, user);
            var userByUserName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == newUser.UserName && x.Id != newUser.Id);
            if (userByUserName != null)
                return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.UserNameIsExist]);
            var result = await _userManager.UpdateAsync(newUser);
            if (!result.Succeeded) return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.UpdateFailed]);
            return GenerateSuccessResponse<string>(_localizer[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<List<ListUserResponse>>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersMapping = _mapper.Map<List<ListUserResponse>>(users);
            var result = GenerateSuccessResponse(usersMapping);
            result.Meta = usersMapping.Count();
            return result;
        }
        public async Task<PaginatedResponse<ListUserPaginateResponse>> GetUsersPaginate(GetListUsersPaginateRequest getListUsersPaginateRequest)
        {
            var users = _userManager.Users.AsQueryable();
            var result = await _mapper.ProjectTo<ListUserPaginateResponse>(users).ToPaginatedListAsync(getListUsersPaginateRequest.PageNumber, getListUsersPaginateRequest.PageSize);

            result.Meta = users.Count();
            return result;
        }
        public async Task<Response<UserResponse>> GetUserById(GetUserByIdRequest getUserByIdRequest)
        {
            var user = await _userManager.Users.AsQueryable().Include(x => x.Orders).ThenInclude(x => x.OrderProducts).ThenInclude(x => x.Product)
                                                      .Include(x => x.Reviews).ThenInclude(x => x.Product)
                                                      .Where(x => x.Id.Equals(getUserByIdRequest.Id)).FirstOrDefaultAsync();
            if (user == null)
                return GenerateNotFoundResponse<UserResponse>();
            var userMapping = _mapper.Map<UserResponse>(user);
            return GenerateSuccessResponse(userMapping);
        }



        public async Task<Response<string>> RegisterUser(UserRegisterRequest userRegisterRequest)
        {
            var user = await _userManager.FindByEmailAsync(userRegisterRequest.Email);
            if (user != null)
                return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.EmailIsExist]);
            var userByUserName = await _userManager.FindByNameAsync(userRegisterRequest.UserName);
            if (userByUserName != null)
                return GenerateBadRequestResponse<string>(_localizer[SharedResourcesKeys.UserNameIsExist]);
            var identityUser = _mapper.Map<User>(userRegisterRequest);
            var createResult = await _userManager.CreateAsync(identityUser, userRegisterRequest.Password);

            if (!createResult.Succeeded)
            {
                var errors = "";
                foreach (var error in createResult.Errors)
                {
                    errors += error.Description.ToString();
                    errors += "-";
                }
                return GenerateBadRequestResponse<string>(errors);
            }
            await _userManager.AddToRoleAsync(identityUser, "User");
            return GenerateCreatedResponse<string>("");

        }
    }
}
