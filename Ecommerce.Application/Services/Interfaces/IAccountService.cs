using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetById(int id);
        Task<IEnumerable<Account>> GetAll();
        Task<Account>Add(AccountRequest accountRequest);
        Task<Account>Update(AccountRequest accountRequest); 
        Task<int>Delete(int id);

    }
}
