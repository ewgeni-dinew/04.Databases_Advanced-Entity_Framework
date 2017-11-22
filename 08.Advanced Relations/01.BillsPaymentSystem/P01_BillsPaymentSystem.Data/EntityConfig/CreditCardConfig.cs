using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    class CreditCardConfig : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.Ignore(e => e.LimitLeft);

            builder.Ignore(e => e.PaymentMethodId);
        }
    }
}
