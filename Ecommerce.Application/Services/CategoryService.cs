using Application.Interfaces;
using Application.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
    {
        public async Task<Category> Add(CategoryRequest categoryRequest)
        {
            if (categoryRequest == null)
                throw new ArgumentNullException();
            Category category = new Category{ 
            Id= categoryRequest.Id,
            Name= categoryRequest.Name,
            Description= categoryRequest.Description,
            };
            var result=await unitOfWork.CategoryRepository.AddAsync(category);
            await unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<int> Delete(int id)
        {
            if (id <0)
                throw new ArgumentOutOfRangeException();
            var entity=await unitOfWork.CategoryRepository.GetByIdAsync(id);
            await unitOfWork.CategoryRepository.DeleteByIdAsync(id);
            await unitOfWork.CompleteAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
           return await unitOfWork.CategoryRepository.GetAllAsync();
        }

        public async Task<Category> GetById(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException();
            return await unitOfWork.CategoryRepository.GetByIdAsync(id);
        }

        public async Task<int> Update(CategoryRequest categoryRequest)
        {
            if (categoryRequest == null)
                throw new ArgumentNullException();
            Category category = new Category
            {
                Id = categoryRequest.Id,
                Name = categoryRequest.Name,
                Description = categoryRequest.Description,
            };
            var result =  unitOfWork.CategoryRepository.Update(category);
            await unitOfWork.CompleteAsync();
            return category.Id;
        }
    }
}
