﻿using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Database.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
           builder.HasKey(x => x.Id);
            builder.Property(x=>x.Quantity).IsRequired();
            builder.HasOne(x=>x.Product).WithMany(x=>x.CartItems).HasForeignKey(x => x.ProductId).IsRequired(false);
        }
    }
}
