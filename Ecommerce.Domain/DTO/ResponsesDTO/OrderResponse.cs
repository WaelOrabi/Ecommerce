namespace Ecommerce.Domain.DTO.ResponsesDTO
{
    public class OrderResponse
    {
        public int NumberProducts { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateAt { get; set; }


        public int AccountId { get; set; }
        public IEnumerable<OrderProductResponse> OrderProducts { get; set; }
    }
}
