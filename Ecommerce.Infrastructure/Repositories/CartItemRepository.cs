using Ecommerce.Application.Repositories;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CartItemRepository(ApplicationDbContext dbContext) : BaseRepository<CartItem>(dbContext), ICartItemRepository
    {

    }
}
