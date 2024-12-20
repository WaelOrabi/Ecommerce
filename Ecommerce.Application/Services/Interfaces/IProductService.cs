using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Add(ProductRequest productRequest);
        Task<Product> Update(ProductRequest productRequesty);
        Task<int> Delete(int id);
    }
}
