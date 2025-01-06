using Ecommerce.Application.Bases;
using Ecommerce.Application.DTO.RequestsDTO.Order;
using Ecommerce.Domain.DTO.ResponsesDTO;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Response<OrderResponse>> GetById(int id);
        Task<Response<IEnumerable<OrderResponse>>> GetAll();
        Task<Response<string>> Add(OrderRequest orderRequest);
        Task<Response<string>> Update(OrderRequest orderRequest, int id);
        Task<Response<int>> Delete(int id);
    }
}
