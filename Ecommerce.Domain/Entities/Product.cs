namespace Ecommerce.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }


        public decimal Price { get; set; }

        public DateTime CreateAt { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
        public IEnumerable<Review> Reviews { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
        public IEnumerable<Image> Images { get; set; }

    }
}

