using Ecommerce.Domain.Entities.Identity;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;

namespace Ecommerce.Infrastructure.RepositoriesImplementation
{
    public class UserRefreshTokenRepository : BaseRepository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
