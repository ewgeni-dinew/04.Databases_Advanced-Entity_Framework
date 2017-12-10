using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.EntityConfig
{
    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CustomerCardId)
                .IsRequired(false);

            builder.HasOne(e => e.CustomerCard)
                .WithMany(c => c.BoughtTickets)
                .HasForeignKey(e => e.CustomerCardId);
        }
    }
}
