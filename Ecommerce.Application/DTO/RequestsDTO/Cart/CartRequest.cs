using Ecommerce.Application.DTO.RequestsDTO.CartItem;

namespace Ecommerce.Application.DTO.RequestsDTO.Cart
{
    public class CartRequest
    {
        public int AccountId { get; set; }
        public List<CartItemRequest> CartItems { get; set; }
    }
}
