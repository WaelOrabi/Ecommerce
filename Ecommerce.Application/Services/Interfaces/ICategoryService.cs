using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category>GetById(int id);
        Task<IEnumerable<Category>> GetAll();
        Task<Category>Add(CategoryRequest category);
        Task<int>Update(CategoryRequest category);
        Task<int>Delete(int id);
    }
}
