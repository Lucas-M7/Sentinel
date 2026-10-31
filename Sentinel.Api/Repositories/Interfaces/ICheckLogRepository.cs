using Sentinel.Api.Entities;

namespace Sentinel.Api.Repositories.Interfaces;

public interface ICheckLogRepository
{
    void Add(CheckLog checkLog);
    Task<IReadOnlyList<CheckLog>> GetLogsByMonitorIdAsync(int monitorId, int limit = 50, CancellationToken ct = default);
}