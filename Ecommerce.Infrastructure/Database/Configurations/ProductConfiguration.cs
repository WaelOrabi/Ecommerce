using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Database.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100).IsRequired(false);
            builder.HasMany(x => x.Reviews).WithOne(x => x.Product).HasForeignKey(x => x.ProductId).IsRequired(false);

        }
    }
}
