using Application.Interfaces;
using Application.Services.Interfaces;
using AutoMapper;
using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Ecommerce.Application.Services
{
    public class RoleService(IUnitOfWork unitOfWork, IMapper mapper) : IRoleService
    {
        private readonly IMapper _mapper = mapper;

        public async Task<RoleResponse> Add(RoleRequestDTO roleRequest)
        {
            if (roleRequest == null)
            {
                throw new ArgumentNullException();
            }
            Role role = _mapper.Map<Role>(roleRequest);
            var result = await unitOfWork.RoleRepository.AddAsync(role);
            await unitOfWork.CompleteAsync();
            return _mapper.Map<RoleResponse>(result);
        }

        public async Task<int> Delete(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException();
            await unitOfWork.RoleRepository.DeleteByIdAsync(id);
            await unitOfWork.CompleteAsync();
            return id;
        }

        public async Task<IEnumerable<RoleResponse>> GetAll()
        {
            var result = await unitOfWork.RoleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleResponse>>(result);
        }

        public async Task<RoleResponse> GetById(int id)
        {
            var result = await unitOfWork.RoleRepository.GetByIdAsync(id);
            return _mapper.Map<RoleResponse>(result);
        }

        public async Task<RoleResponse?> Update(RoleRequestDTO roleRequest, int id)
        {
            var role = await unitOfWork.RoleRepository.GetByIdAsync(id);
            if (role == null)
            {
                return null;
            }
            role = _mapper.Map<Role>(roleRequest);
            unitOfWork.RoleRepository.Update(role);
            await unitOfWork.CompleteAsync();
            return _mapper.Map<RoleResponse>(role);
        }

    }
}
