namespace Ecommerce.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }



        public int RoleId { get; set; }
        public Role Role { get; set; }


        public int? AddressId { get; set; }
        public Address Address { get; set; }

        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public Cart Cart { get; set; }
    }
}
