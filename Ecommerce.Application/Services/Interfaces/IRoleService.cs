using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Role> GetById(int id);
        Task<IEnumerable<Role>> GetAll();
        Task<Role> Add(RoleRequest roleRequest);
        Task<Role> Update(RoleRequest roleRequest);
        Task<int> Delete(int id);
    }
}
