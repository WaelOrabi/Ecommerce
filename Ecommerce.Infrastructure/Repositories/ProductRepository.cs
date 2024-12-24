using Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        public async Task<List<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
        {
           return await context.Products.Where(predicate).ToListAsync();
        }
    }
}
