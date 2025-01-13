using Ecommerce.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int NumberProducts { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateAt { get; set; }


        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}
