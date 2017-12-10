using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.Config
{
    public class UserFollowerConfig : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.HasKey(e => new { e.UserId, e.FollowerId });

            builder.HasOne(e => e.User)
                .WithMany(u => u.Followers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Follower)
                .WithMany(u => u.UsersFollowing)
                .HasForeignKey(u => u.FollowerId);
        }
    }
}
