﻿using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.Config
{
    class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasAlternateKey(e => e.Username);

            builder.Property(u => u.Username)
                .HasMaxLength(30);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(u => u.ProfilePicture)
                .WithMany(p => p.Users)
                .HasForeignKey(u=>u.ProfilePictureId);
        }
    }
}
