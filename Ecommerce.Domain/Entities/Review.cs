using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; } 

     
        public int ProductId { get; set; }
        public Product Product { get; set; }

      
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }

}
