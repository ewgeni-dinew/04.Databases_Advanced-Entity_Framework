using FastFood.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Data.EntityConfig
{
    class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Ignore(e => e.TotalPrice);

            builder.HasOne(e => e.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.EmployeeId);
        }
    }
}
