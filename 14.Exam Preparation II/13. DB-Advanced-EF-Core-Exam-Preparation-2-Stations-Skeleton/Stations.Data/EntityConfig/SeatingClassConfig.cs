using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.EntityConfig
{
    public class SeatingClassConfig : IEntityTypeConfiguration<SeatingClass>
    {
        public void Configure(EntityTypeBuilder<SeatingClass> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.Name);

            builder.HasAlternateKey(e => e.Abbreviation);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);
           
        }
    }
}
