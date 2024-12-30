using Microsoft.AspNetCore.Http;

namespace Ecommerce.Domain.ServiceModel.Requests
{
    public class ProductRequestDTO
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }


        public decimal Price { get; set; }

        public DateTime CreateAt { get; set; }
        public IFormFile Image { get; set; }


        public int CategoryId { get; set; }
    }
}
