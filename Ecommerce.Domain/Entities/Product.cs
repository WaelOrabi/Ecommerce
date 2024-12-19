using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

    
        public string Name { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }

  
        public decimal Price { get; set; }

        public DateTime CreateAt { get; set; }
        public List<string> ListImages { get; set; } 

 
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }
        public IEnumerable<Review> Reviews { get; set; }

    }
}

