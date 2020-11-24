using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Models.Base;
using MjCommerce.Shared.Models.Identity;
using MjCommerce.Shared.Models.Orders;

namespace MjCommerce.Shared
{
    public class MjCommerceDbContext : IdentityDbContext<User>
    {
        public MjCommerceDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PhotoBase> Photos { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            builder.Entity<PhotoBase>().HasIndex(p => p.Name).IsUnique();

            base.OnModelCreating(builder);
        }
    }
}