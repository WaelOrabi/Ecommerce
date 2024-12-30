namespace Ecommerce.Domain.ServiceModel.Requests
{
    public class OrderRequestDTO
    {
        public int AccountId { get; set; }
        public List<OrderProductRequestDTO> OrderProducts { get; set; }
    }
}
