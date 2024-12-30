namespace Ecommerce.Domain.ServiceModel.Requests
{
    public class AccountRequest
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int AddressId { get; set; }

    }
}
