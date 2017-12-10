using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.Config
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Content)
                .HasMaxLength(250);

            builder.HasOne(e => e.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(e => e.PostId);

            builder.HasOne(e => e.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(e=>e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
