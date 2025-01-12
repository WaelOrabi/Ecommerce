using Ecommerce.Domain.Entities.Identity;

namespace Ecommerce.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }
    }

}
