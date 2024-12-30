namespace Ecommerce.Domain.DTO.ResponsesDTO
{
    public class CartItemResponse
    {
        public int? CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
