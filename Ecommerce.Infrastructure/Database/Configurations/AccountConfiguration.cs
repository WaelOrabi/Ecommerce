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
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(10).IsRequired();
            builder.Property(x=>x.LastName).HasMaxLength(10).IsRequired();
            builder.HasOne(x => x.Role).WithMany(x => x.Accounts).HasForeignKey(x=>x.RoleId).IsRequired(true);
            builder.HasOne(x=>x.Address).WithMany(x=>x.Accounts).HasForeignKey(x => x.AddressId).IsRequired(true);
            builder.HasMany(x => x.Reviews).WithOne(x => x.Account).HasForeignKey(x => x.AccountId).IsRequired(false);
            builder.HasMany(x => x.Orders).WithOne(x => x.Account).HasForeignKey(x => x.AccountId).IsRequired(false);
         
        }
    }
}
