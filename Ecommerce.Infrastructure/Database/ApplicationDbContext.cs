using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Account>Accounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Order>Orders { get; set; }
        public DbSet<OrderProduct>OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review>  Reviews { get; set; }
        public DbSet<Role>Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
   
    }
}
