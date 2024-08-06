using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infra.EntitiesConfiguration;
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasIndex(e => e.ISBN)
            .IsUnique();

        builder.Property(e => e.Title)
            .HasMaxLength(100);

        builder.Property(e => e.Author)
            .HasMaxLength(100);

        builder.HasOne(e => e.Category)
            .WithMany(b => b.Books)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
