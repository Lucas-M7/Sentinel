using Monitor = Sentinel.Api.Entities.Monitor;

namespace Sentinel.Api.Repositories.Interfaces;

public interface IMonitorRepository
{
    Task<IReadOnlyList<Monitor>> GetAllAsync(CancellationToken ct = default);
    Task<Monitor?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> ExistsAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Monitor>> GetMonitorsReadyForCheckAsync(DateTimeOffset currentTime, CancellationToken ct = default);
    void Add(Monitor monitor);
    void Update(Monitor monitor);
    void Remove(Monitor monitor);
}