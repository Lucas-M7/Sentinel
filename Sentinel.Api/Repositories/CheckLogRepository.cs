using Microsoft.EntityFrameworkCore;
using Sentinel.Api.Data;
using Sentinel.Api.Entities;
using Sentinel.Api.Repositories.Interfaces;

namespace Sentinel.Api.Repositories;

public class CheckLogRepository(AppDbContext context) : ICheckLogRepository
{
    public void Add(CheckLog checkLog) => context.CheckLogs.Add(checkLog);

    public async Task<IReadOnlyList<CheckLog>> GetLogsByMonitorIdAsync(
        int monitorId, int limit = 50, CancellationToken ct = default)
        => await context.CheckLogs
            .AsNoTracking()
            .Where(l => l.MonitorId == monitorId)
            .OrderByDescending(l => l.CreatedAt)
            .Take(limit)
            .ToListAsync(ct);
}