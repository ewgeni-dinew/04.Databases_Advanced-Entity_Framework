using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.FirstName)
            .IsRequired()
            .IsUnicode();

            builder.Property(e => e.LastName)
            .IsRequired()
            .IsUnicode();

            builder.Property(e => e.Email)
            .IsRequired()
            .IsUnicode(false);

            builder.Property(e => e.Password)
            .IsRequired()
            .IsUnicode(false);
        }
    }
}
