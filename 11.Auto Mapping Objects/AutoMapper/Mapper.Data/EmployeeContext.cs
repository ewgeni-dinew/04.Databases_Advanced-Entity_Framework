using Mapper.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Mapper.Data
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext()
        { }
        public EmployeeContext(DbContextOptions options)
            :base(options){}

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ServerConfig.ConnectionStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity=> 
            {
                entity.HasKey(e=>e.Id);

                entity.Property(e => e.FirstName)
                .IsRequired();

                entity.Property(e => e.LastName)
                .IsRequired();

                entity.Property(e => e.Address)
                .IsRequired(false);
            });
        }
    }
}
