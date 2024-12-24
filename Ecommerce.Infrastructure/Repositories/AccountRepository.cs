using Application.Interfaces;
using Ecommerce.Domain;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel.Requests;
using Ecommerce.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class AccountRepository(ApplicationDbContext context) : BaseRepository<Account>(context),IAccountRepository
    {
        public async Task<Account> Auth(AuthAccount authAccount)
        {
            var account = await context.Set<Account>()
          .Include(a => a.Role) 
          .FirstOrDefaultAsync(a => a.Email == authAccount.Email && a.Password == authAccount.Password);
            return account!;

        }

        public async Task<bool> HasPermission(int userId, Permission permission)
        {
            return await context.Set<AccountPermission>().AnyAsync(a => a.UserId == userId&& a.PermissionId==permission);
        }
    }
}
