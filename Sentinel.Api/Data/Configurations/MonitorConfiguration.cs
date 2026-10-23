using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitor = Sentinel.Api.Entities.Monitor;

namespace Sentinel.Api.Data.Configurations;

public class MonitorConfiguration : IEntityTypeConfiguration<Monitor>
{
    public void Configure(EntityTypeBuilder<Monitor> builder)
    {
        builder.ToTable("Monitors");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name).IsRequired().HasMaxLength(Monitor.NameMaxLength);
        builder.Property(m => m.Url).IsRequired().HasMaxLength(Monitor.UrlMaxLength);

        // sustenta a query do Worker: WHERE IsActive AND NextCheckAt <= now
        builder.HasIndex(m => new { m.IsActive, m.NextCheckAt });

        builder.HasMany(m => m.CheckLogs)
            .WithOne(l => l.Monitor)
            .HasForeignKey(l => l.MonitorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}