﻿using Ecommerce.Domain.Entities.Identity;

namespace Ecommerce.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }

}
