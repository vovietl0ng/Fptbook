using Fptbook.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fptbook.Models.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(t => new { t.UserId, t.CartId });

            builder.ToTable("Orders");

            builder.HasOne(t => t.AppUser).WithMany(pc => pc.Orders)
                .HasForeignKey(pc => pc.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Cart).WithMany(pc => pc.Orders)
              .HasForeignKey(pc => pc.CartId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
