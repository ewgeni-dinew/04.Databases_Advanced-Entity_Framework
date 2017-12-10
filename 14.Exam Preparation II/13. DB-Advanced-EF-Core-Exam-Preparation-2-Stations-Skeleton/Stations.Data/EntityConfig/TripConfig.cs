using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.EntityConfig
{
    public class TripCongif : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.TimeDifference)
                .IsRequired(false);

            builder.HasOne(e => e.DestinationStation)
                .WithMany(d => d.TripsTo)
                .HasForeignKey(e => e.DestinationStationId);

            builder.HasOne(e => e.OriginStation)
                .WithMany(os => os.TripsFrom)
                .HasForeignKey(e => e.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Train)
                .WithMany(t => t.Trips)
                .HasForeignKey(e => e.TrainId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
