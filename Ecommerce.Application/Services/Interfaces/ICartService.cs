using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartResponse> GetById(int id);
        Task<IEnumerable<CartResponse>> GetAll();
        Task<CartResponse> Add(CartRequestDTO cartRequest);
        Task<CartResponse?> Update(CartRequestDTO cartRequest, int id);
        Task<int> Delete(int id);
    }
}
