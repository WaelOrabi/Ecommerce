using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Database.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Street).HasMaxLength(50).IsRequired();
            builder.Property(x => x.City).HasMaxLength(30).IsRequired(false);
            builder.Property(x => x.State).HasMaxLength(30).IsRequired(false);
            builder.Property(x=>x.Country).HasMaxLength(30).IsRequired(false);
            builder.Property(x=>x.PostalCode).HasMaxLength(10).IsRequired(false);
        }
    }
}
