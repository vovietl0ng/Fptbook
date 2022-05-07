using Fptbook.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fptbook.Models.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.HasOne(t => t.User).WithMany(pc => pc.CartItems)
                .HasForeignKey(pc => pc.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Book).WithMany(pc => pc.CartItems)
                .HasForeignKey(pc => pc.BookId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Order).WithMany(pc => pc.CartItems)
                 .HasForeignKey(pc => pc.OrderId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
