using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Database.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FileName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.FileDescription).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.FileExtension).HasMaxLength(5).IsRequired();
            builder.Property(x => x.FileSizeInBytes).IsRequired();
            builder.Property(x => x.FilePath).HasMaxLength(100).IsRequired();
            builder.HasOne(x => x.Product).WithMany(x => x.Images).HasForeignKey(x => x.ProductId).IsRequired();
        }
    }
}
//public string? FileDescription { get; set; }
//public string FileExtension { get; set; }
//public long FileSizeInBytes { get; set; }
//public string FilePath { get; set; }
