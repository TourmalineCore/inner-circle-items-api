using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Mappings;

public class WorkEntriesMapping : IEntityTypeConfiguration<WorkEntry>
{
    public void Configure(EntityTypeBuilder<WorkEntry> builder)
    {
        builder
            .Property(p => p.Duration)
            .HasComputedColumnSql("end_time - start_time", stored: true);

        builder
            .Property(e => e.StartTime)
            .HasColumnType("timestamp without time zone");

        builder
            .Property(e => e.EndTime)
            .HasColumnType("timestamp without time zone");

        builder
            .ToTable(b => b.HasCheckConstraint("ck_work_entries_type_not_zero", "\"type\" <> 0"));
    }
}
