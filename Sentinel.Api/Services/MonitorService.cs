using Sentinel.Api.Repositories.Interfaces;
using Sentinel.Api.Services.Interfaces;
using Monitor = Sentinel.Api.Entities.Monitor;

namespace Sentinel.Api.Services;

public class MonitorService(
    IMonitorRepository repository,
    ICheckLogRepository checkLogRepository) : IMonitorService
{
    private readonly IMonitorRepository _repository = repository;
    private readonly ICheckLogRepository _checkLogRepository = checkLogRepository;

    public async Task<Monitor> CreateMonitorAsync(string name, string url, int intervalSeconds)
    {
        var newMonitor = new Monitor(name, url, intervalSeconds, DateTimeOffset.UtcNow);

        await _repository.AddAsync(newMonitor);
        return newMonitor;
    }

    public async Task<IEnumerable<Monitor>> GetAllMonitorsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<Entities.CheckLog>> GetMonitorLogsAsync(int monitorId, int limit = 50)
    {
        return await _checkLogRepository.GetLogsByMonitorIdAsync(monitorId, limit);
    }
}