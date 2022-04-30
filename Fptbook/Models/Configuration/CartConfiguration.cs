﻿using Fptbook.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fptbook.Models.Configuration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.TotalPrice).IsRequired();
            builder.HasOne(t => t.AppUser).WithMany(pc => pc.Carts)
               .HasForeignKey(pc => pc.UserId);
        }
    }
}
