using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;

namespace Ecommerce.Infrastructure.RepositoriesImplementation
{
    public class RoleRepository(ApplicationDbContext context) : BaseRepository<Role>(context), IRoleRepository
    {
    }
}
