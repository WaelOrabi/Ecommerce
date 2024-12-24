using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Task<List<Product>> GetAllAsync(Expression<Func<Product,bool>>predicate);
    }
}
