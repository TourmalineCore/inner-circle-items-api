using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Mappings;

public class ItemTypeMapping : IEntityTypeConfiguration<ItemType>
{
    public void Configure(EntityTypeBuilder<ItemType> builder)
    {
        builder
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder
            .HasIndex(e => e.Name)
            .IsUnique();
    }
}