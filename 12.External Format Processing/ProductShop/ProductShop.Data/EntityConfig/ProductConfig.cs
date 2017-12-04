using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductShop.Models;

namespace ProductShop.Data.EntityConfig
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.BuyerId)
                .IsRequired(false);

            builder.HasOne(e => e.Seller)
                .WithMany(s => s.ProductsSold)
                .HasForeignKey(e => e.SellerId);

            builder.HasOne(e => e.Buyer)
                .WithMany(b =>b.ProductsBought )
                .HasForeignKey(e => e.BuyerId);
        }
    }
}
