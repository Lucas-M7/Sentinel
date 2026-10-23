using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sentinel.Api.Entities;

namespace Sentinel.Api.Data.Configurations;

public class CheckLogConfiguration : IEntityTypeConfiguration<CheckLog>
{
    public void Configure(EntityTypeBuilder<CheckLog> builder)
    {
        builder.ToTable("CheckLogs");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Response).IsRequired().HasMaxLength(CheckLog.ResponseMaxLength);

        // sustenta o histórico: WHERE MonitorId = x ORDER BY CreatedAt DESC
        builder.HasIndex(l => new { l.MonitorId, l.CreatedAt })
            .IsDescending(false, true);
    }
}