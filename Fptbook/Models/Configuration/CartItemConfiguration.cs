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
            builder.HasKey(t => new { t.BookId, t.CartId });

            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.HasOne(t => t.Cart).WithMany(pc => pc.CartItems)
                .HasForeignKey(pc => pc.CartId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Book).WithOne(pc => pc.CartItem)
                .HasForeignKey<CartItem>(pc => pc.BookId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
