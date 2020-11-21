using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MjCommerce.Shared.Models;
using MjCommerce.Shared.Models.Base;

namespace MjCommerce.Shared
{
    public class MjCommerceDbContext : IdentityDbContext
    {
        public MjCommerceDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PhotoBase> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            builder.Entity<PhotoBase>().HasIndex(p => p.Name).IsUnique();

            base.OnModelCreating(builder);
        }
    }
}