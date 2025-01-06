using Ecommerce.Domain.DTO.ResponsesDTO;

namespace Ecommerce.Domain.DTO.Responses
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<OrderResponse> Orders { get; set; }
        public AddressResponse Address { get; set; }
        public RoleResponse Role { get; set; }

    }
}
