using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.Infrastructure.RepositoriesImplementation
{
    public class ProductRepository(ApplicationDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        public async Task<List<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
        {
            return await context.Products.Where(predicate).ToListAsync();
        }
    }
}
