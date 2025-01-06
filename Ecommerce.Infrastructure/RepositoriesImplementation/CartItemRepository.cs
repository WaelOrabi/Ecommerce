using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;

namespace Ecommerce.Infrastructure.RepositoriesImplementation
{
    public class CartItemRepository(ApplicationDbContext dbContext) : BaseRepository<CartItem>(dbContext), ICartItemRepository
    {

    }
}
