using Ecommerce.Domain;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountRepository:IBaseRepository<Account>
    {
       public  Task<Account> Auth(AuthAccount authAccount);
        public Task<bool> HasPermission(int userId, Permission permission);
    }
}
