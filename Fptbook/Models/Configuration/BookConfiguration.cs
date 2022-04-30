using Fptbook.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fptbook.Models.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Author).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
            builder.Property(x => x.Quanlity).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.ISBN).IsRequired();
            builder.HasOne(t => t.Store).WithMany(pc => pc.Books)
                .HasForeignKey(pc => pc.StoreId);
            builder.HasOne(t => t.Category).WithMany(pc => pc.Books)
                .HasForeignKey(pc => pc.CategoryId);
            
        }
    }
}
