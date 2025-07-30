using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Mappings;

public class ItemMaping : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder
            .HasIndex(x => x.Id)
            .IsUnique();

        builder
            .Property(e => e.TenantId)
            .IsRequired();

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder
            .Property(x => x.SerialNumber)
            .HasMaxLength(128);

        builder
            .Property(x => x.ItemTypeId)
            .IsRequired();

        builder
            .HasOne(x => x.ItemType)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.ItemTypeId);

        builder
            .Property(x => x.Price)
            .IsRequired();
    }
}