using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Product;
using Ecommerce.Domain.DTO.ResponsesDTO;

namespace Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<Response<ProductResponse>> GetById(int id);
        Task<Response<IEnumerable<ProductResponse>>> GetAll();
        Task<Response<string>> Add(ProductRequest productRequest);
        Task<Response<string>> Update(ProductRequest productRequesty, int id);
        Task<Response<int>> Delete(int id);
        Task<bool> IsProductExist(int id);
    }
}
