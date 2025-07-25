using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Mappings;

public class DelistedItemRecordMapping : IEntityTypeConfiguration<DelistedItemRecord>
{
    public void Configure(EntityTypeBuilder<DelistedItemRecord> builder)
    {
        builder
            .HasKey(e => e.ItemId);

        builder
            .HasOne(e => e.Item)
            .WithOne(e => e.DelistedItemRecord);
    }
}