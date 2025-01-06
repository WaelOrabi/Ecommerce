using Ecommerce.Domain;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.RepositoriesImplementation
{
    public class AccountRepository(ApplicationDbContext context) : BaseRepository<Account>(context), IAccountRepository
    {
        public async Task<Account> Auth(string Email, string Password)
        {
            var account = await context.Set<Account>()
          .Include(a => a.Role)
          .FirstOrDefaultAsync(a => a.Email == Email && a.Password == Password);
            return account!;

        }

        public async Task<bool> HasPermission(int userId, Permission permission)
        {
            return await context.Set<AccountPermission>().AnyAsync(a => a.UserId == userId && a.PermissionId == permission);
        }
    }
}
