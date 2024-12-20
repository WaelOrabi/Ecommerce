using Application.Interfaces;
using Application.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class RoleService(IUnitOfWork unitOfWork) : IRoleService
    {
        public async Task<Role> Add(RoleRequest roleRequest)
        {
            if (roleRequest == null)
            {
                throw new ArgumentNullException();
            }
            Role role = new Role { 
            Id=roleRequest.Id,
            Name=roleRequest.Name,
            };
            var result= await unitOfWork.RoleRepository.AddAsync(role);
            await unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<int> Delete(int id)
        {
            if(id<0)
                throw new ArgumentOutOfRangeException();
            await unitOfWork.RoleRepository.DeleteByIdAsync(id);
            await unitOfWork.CompleteAsync();
            return id;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await unitOfWork.RoleRepository.GetAllAsync();
        }

        public async Task<Role> GetById(int id)
        {
            return await unitOfWork.RoleRepository.GetByIdAsync(id);
        }

        public async Task<Role> Update(RoleRequest roleRequest)
        {
            if (roleRequest == null)
            {
                throw new ArgumentNullException();
            }
            Role role = new Role
            {
                Id = roleRequest.Id,
                Name = roleRequest.Name,
            };
            var result=  unitOfWork.RoleRepository.Update(role);
            await unitOfWork.CompleteAsync();
            return result;
        }
    }
}
