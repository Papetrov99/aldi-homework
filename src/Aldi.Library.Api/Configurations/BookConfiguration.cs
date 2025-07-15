using Aldi.Library.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aldi.Library.Api.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.PublishedYear)
            .IsRequired();
        builder.Property(b => b.IsAvailable)
            .IsRequired();
        builder.Property(b => b.Title)
            .IsRequired();
        builder.Property(b => b.Author)
            .IsRequired();
        builder.Property(b => b.ISBN)
            .IsRequired();
    }
}