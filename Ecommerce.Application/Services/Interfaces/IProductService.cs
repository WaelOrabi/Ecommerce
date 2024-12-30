using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse> GetById(int id);
        Task<IEnumerable<ProductResponse>> GetAll();
        Task<ProductResponse> Add(ProductRequestDTO productRequest);
        Task<ProductResponse?> Update(ProductRequestDTO productRequesty, int id);
        Task<int> Delete(int id);
    }
}
