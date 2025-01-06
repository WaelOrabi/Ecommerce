using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Category;
using Ecommerce.Domain.DTO.ResponsesDTO;

namespace Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Response<CategoryResponse>> GetById(int id);
        Task<bool> IsCategoryExist(int id);
        Task<Response<IEnumerable<CategoryResponse>>> GetAll();
        Task<Response<string>> Add(CategoryRequest category);
        Task<Response<string>> Update(CategoryRequest category, int id);
        Task<Response<int>> Delete(int id);
    }
}
