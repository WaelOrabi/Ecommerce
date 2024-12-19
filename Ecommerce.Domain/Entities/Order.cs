using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int NumberProducts { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateAt { get; set; }

       
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}
