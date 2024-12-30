using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponse> GetById(int id);
        Task<IEnumerable<CategoryResponse>> GetAll();
        Task<CategoryResponse> Add(CategoryRequestDTO category);
        Task<CategoryResponse?> Update(CategoryRequestDTO category, int id);
        Task<int> Delete(int id);
    }
}
