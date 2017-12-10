using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.EntityConfig
{
    public class TrainSeatConfig : IEntityTypeConfiguration<TrainSeat>
    {
        public void Configure(EntityTypeBuilder<TrainSeat> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Train)
                .WithMany(t => t.TrainSeats)
                .HasForeignKey(e => e.TrainId);

           
        }
    }
}
