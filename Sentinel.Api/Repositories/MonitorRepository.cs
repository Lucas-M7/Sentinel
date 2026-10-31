using Microsoft.EntityFrameworkCore;
using Sentinel.Api.Data;
using Sentinel.Api.Repositories.Interfaces;
using Monitor = Sentinel.Api.Entities.Monitor;

namespace Sentinel.Api.Repositories;

public class MonitorRepository(AppDbContext context) : IMonitorRepository
{
    public async Task<IReadOnlyList<Monitor>> GetAllAsync(CancellationToken ct = default)
        => await context.Monitors.AsNoTracking().OrderBy(m => m.Id).ToListAsync(ct);

    public Task<Monitor?> GetByIdAsync(int id, CancellationToken ct = default)
        => context.Monitors.FirstOrDefaultAsync(m => m.Id == id, ct);

    public Task<bool> ExistsAsync(int id, CancellationToken ct = default)
        => context.Monitors.AnyAsync(m => m.Id == id, ct);

    public async Task<IReadOnlyList<Monitor>> GetMonitorsReadyForCheckAsync(
        DateTimeOffset currentTime, CancellationToken ct = default)
        => await context.Monitors
            .Where(m => m.IsActive && m.NextCheckAt <= currentTime)
            .AsNoTracking()
            .ToListAsync(ct);

    public void Add(Monitor monitor) => context.Monitors.Add(monitor);
    public void Update(Monitor monitor) => context.Monitors.Update(monitor);
    public void Remove(Monitor monitor) => context.Monitors.Remove(monitor);
}