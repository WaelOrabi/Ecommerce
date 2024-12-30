namespace Ecommerce.Domain.ServiceModel.Requests
{
    public class CartRequestDTO
    {
        public int AccountId { get; set; }
        public List<CartItemRequestDTO> CartItems { get; set; }
    }
}
