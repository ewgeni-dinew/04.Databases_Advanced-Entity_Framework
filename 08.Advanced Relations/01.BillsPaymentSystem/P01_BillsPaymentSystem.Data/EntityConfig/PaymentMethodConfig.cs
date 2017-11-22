using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasIndex(e => new { e.BankAccountId, e.UserId, e.CreditCardId })
                .IsUnique();

            builder.HasOne(e => e.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.BankAccount)
                .WithOne(ba =>ba.PaymentMethod)
                .HasForeignKey<PaymentMethod>(e=>e.BankAccountId);

            builder.HasOne(e => e.CreditCard)
                .WithOne(cc => cc.PaymentMethod)
                .HasForeignKey<PaymentMethod>(e => e.CreditCardId);
        }
    }
}
