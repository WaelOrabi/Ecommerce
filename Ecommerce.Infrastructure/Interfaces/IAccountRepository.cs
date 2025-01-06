using Ecommerce.Domain;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Infrastructure.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        public Task<Account> Auth(string Email, String Password);
        public Task<bool> HasPermission(int userId, Permission permission);
    }
}
