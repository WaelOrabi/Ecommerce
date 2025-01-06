namespace Ecommerce.Domain.DTO.ResponsesDTO
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }


        public decimal Price { get; set; }

        public DateTime CreateAt { get; set; }
        public byte[] Image { get; set; }


    }
}
