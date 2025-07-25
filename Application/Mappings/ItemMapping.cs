using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Mappings;

public class ItemMapping : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {

        builder
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder
            .HasIndex(e => e.Name)
            .IsUnique();

        builder.
            Property(e => e.SerialNumber);

        builder
            .Property(e => e.ItemTypeId)
            .IsRequired();

        builder
            .Property(e => e.Price)
            .IsRequired();

        builder
            .Property(e => e.PurchaseDate)
            .IsRequired();

        builder
            .Property(e => e.Description);

        builder
            .Property(e => e.HolderId);

        builder
            .Property(e => e.IsDeleted);

        builder
            .Property(e => e.isRemoved);

        builder
            .Property(e => e.Status);

    }
}