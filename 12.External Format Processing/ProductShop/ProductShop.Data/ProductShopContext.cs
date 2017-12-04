using Microsoft.EntityFrameworkCore;
using ProductShop.Data.EntityConfig;
using ProductShop.Models;
using System;

namespace ProductShop.Data
{
    public class ProductShopContext:DbContext
    {
        public ProductShopContext(){ }
        public ProductShopContext(DbContextOptions options)
            :base(options){ }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbConfigure.ConnectionStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());

            modelBuilder.ApplyConfiguration(new CategoryConfig());

            modelBuilder.ApplyConfiguration(new ProductConfig());

            modelBuilder.ApplyConfiguration(new CatProdConfig());
        }
    }
}
