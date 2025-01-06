using Application.Services.Interfaces;
using AutoMapper;
using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Role;
using Ecommerce.Application.Resources;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Ecommerce.Application.Services.Implementation
{
    public class RoleService(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResources> localizer) : ResponseHandler(localizer), IRoleService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IStringLocalizer<SharedResources> _localizer = localizer;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Add(RoleRequest roleRequest)
        {

            Role role = _mapper.Map<Role>(roleRequest);
            await unitOfWork.RoleRepository.AddAsync(role);
            await unitOfWork.CompleteAsync();
            return GenerateSuccessResponse<string>("");
        }

        public async Task<Response<int>> Delete(int id)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            if (role == null)
                return GenerateNotFoundResponse<int>();
            await unitOfWork.RoleRepository.DeleteByIdAsync(role);
            await unitOfWork.CompleteAsync();
            return GenerateSuccessResponse(id);
        }

        public async Task<Response<IEnumerable<RoleResponse>>> GetAll()
        {

            var result = await _unitOfWork.RoleRepository.GetAllAsync();
            var resultMapping = _mapper.Map<IEnumerable<RoleResponse>>(result);
            return GenerateSuccessResponse(resultMapping);
        }

        public async Task<Response<RoleResponse>> GetById(int id)
        {
            var result = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            if (result == null)
                return GenerateNotFoundResponse<RoleResponse>();
            var resultMapping = _mapper.Map<RoleResponse>(result);
            return GenerateSuccessResponse(resultMapping);
        }

        public async Task<bool> IsRoleExist(int id)
        {

            var result = await _unitOfWork.RoleRepository.GetTableNoTracking().AnyAsync(x => x.Id.Equals(id));
            return result;

        }

        public async Task<Response<string>> Update(RoleRequest roleRequest, int id)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            if (role == null)
                return GenerateNotFoundResponse<string>();
            role = _mapper.Map(roleRequest, role);
            _unitOfWork.RoleRepository.Update(role);
            await _unitOfWork.CompleteAsync();
            return GenerateSuccessResponse("");
        }

    }
}
