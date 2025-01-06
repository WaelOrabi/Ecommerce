using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.RepositoriesImplementation
{
    public class CartRepository(ApplicationDbContext dbContext) : BaseRepository<Cart>(dbContext), ICartRepository
    {
        public async Task<Cart> GetByIdIncludeCartItemAsync(int id)
        {
            return await dbContext.Set<Cart>().Include(c => c.CartItems).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
