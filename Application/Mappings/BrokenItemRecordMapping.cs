using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Mappings;

public class BrokenItemRecordMapping : IEntityTypeConfiguration<BrokenItemRecord>
{
    public void Configure(EntityTypeBuilder<BrokenItemRecord> builder)
    {
        builder
            .Property(x => x.ItemId)
            .IsRequired();

        builder
            .HasOne(e => e.Item)
            .WithMany(e => e.BrokenItemRecords);
    }
}