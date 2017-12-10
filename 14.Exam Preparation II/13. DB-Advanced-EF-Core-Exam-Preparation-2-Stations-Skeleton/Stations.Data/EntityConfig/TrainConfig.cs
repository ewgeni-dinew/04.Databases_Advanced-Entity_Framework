using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.EntityConfig
{
    public class TrainConfig : IEntityTypeConfiguration<Train>
    {
        public void Configure(EntityTypeBuilder<Train> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.TrainNumber);

            builder.Property(e => e.TrainNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Type)
                .IsRequired(false);
        }
    }
}
