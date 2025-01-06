namespace Ecommerce.Domain.DTO.ResponsesDTO
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string? City { get; set; }

        public string? Country { get; set; }
    }
}
