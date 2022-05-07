using Fptbook.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fptbook.Models.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.ToTable("Orders");

            builder.HasOne(t => t.AppUser).WithMany(pc => pc.Orders)
                .HasForeignKey(pc => pc.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Status).IsRequired();
        }
    }
}
