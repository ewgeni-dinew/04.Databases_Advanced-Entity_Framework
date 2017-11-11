using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data;
using P03_SalesDatabase.Data.Models;
using System;

namespace P03_SalesDatabase.Data
{
    public class SalesContext:DbContext
    {
        public SalesContext()
        { }

        public SalesContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //updated for task 04.
            modelBuilder.Entity<Product>(entity=>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);
            
                //added for task 04.-->
                entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasDefaultValue("No description");
                //<--

            });

            modelBuilder.Entity<Customer>(entity=>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

                entity.Property(e => e.Email)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(80);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(80);
            });

            //updated for task 05.
            modelBuilder.Entity<Sale>(entity => 
            {
                entity.HasKey(e => e.SaleId);

                //added for task 05.-->
                entity.Property(e => e.Date)
                .HasDefaultValueSql("GETDATE()");
                //<--

                entity.HasOne(e => e.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(e => e.CustomerId);

                entity.HasOne(e => e.Store)
                .WithMany(c => c.Sales)
                .HasForeignKey(e => e.StoreId);

                entity.HasOne(e => e.Product)
                .WithMany(c => c.Sales)
                .HasForeignKey(e => e.ProductId);
   
            });
        }
    }
}
