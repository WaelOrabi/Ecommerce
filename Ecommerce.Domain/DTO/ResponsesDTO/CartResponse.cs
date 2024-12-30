namespace Ecommerce.Domain.DTO.ResponsesDTO
{
    public class CartResponse
    {
        public int AccountId { get; set; }
        public List<CartItemResponse> CartItems { get; set; }
    }
}
