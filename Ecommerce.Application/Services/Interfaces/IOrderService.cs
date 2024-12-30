using Ecommerce.Domain.DTO.ResponsesDTO;
using Ecommerce.Domain.ServiceModel.Requests;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> GetById(int id);
        Task<IEnumerable<OrderResponse>> GetAll();
        Task<OrderResponse> Add(OrderRequestDTO orderRequest);
        Task<OrderResponse?> Update(OrderRequestDTO orderRequest, int id);
        Task<int> Delete(int id);
    }
}
