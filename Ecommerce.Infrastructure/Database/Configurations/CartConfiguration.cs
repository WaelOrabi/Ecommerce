using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Database.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithOne(x => x.Cart).HasForeignKey<Cart>(x => x.UserId).IsRequired(true);
            builder.HasMany(x => x.CartItems).WithOne(x => x.Cart).HasForeignKey(x => x.CartId).IsRequired(false).OnDelete(DeleteBehavior.SetNull); ;
        }
    }
}
