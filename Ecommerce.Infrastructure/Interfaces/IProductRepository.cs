using Ecommerce.Domain.Entities;
using System.Linq.Expressions;

namespace Ecommerce.Infrastructure.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate);
    }
}
