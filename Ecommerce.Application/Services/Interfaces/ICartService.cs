using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Cart;
using Ecommerce.Domain.DTO.ResponsesDTO;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<Response<CartResponse>> GetById(int id);
        Task<Response<IEnumerable<CartResponse>>> GetAll();
        Task<Response<string>> Add(CartRequest cartRequest);
        Task<Response<string>> Update(CartRequest cartRequest, int id);
        Task<Response<int>> Delete(int id);
    }
}
