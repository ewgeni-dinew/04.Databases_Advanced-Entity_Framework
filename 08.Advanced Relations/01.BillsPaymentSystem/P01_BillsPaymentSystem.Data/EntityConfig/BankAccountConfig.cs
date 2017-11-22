using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.Property(e => e.BankName)
                .IsRequired()
                .IsUnicode();

            builder.Property(e => e.SwiftCode)
               .IsRequired()
               .IsUnicode(false);

            builder.Ignore(e => e.PaymentMethodId);
        }
    }
}
