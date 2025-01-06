using Ecommerce.Application.DTO.RequestsDTO.OrderProduct;

namespace Ecommerce.Application.DTO.RequestsDTO.Order
{
    public class OrderRequest
    {
        public int AccountId { get; set; }
        public List<OrderProductRequest> OrderProducts { get; set; }
    }
}
