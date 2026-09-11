using Sentinel.Api.Repositories.Interfaces;
using Sentinel.Api.Services.Interfaces;
using Monitor = Sentinel.Api.Entities.Monitor;

namespace Sentinel.Api.Services;

public class MonitorService(IMonitorRepository repository) : IMonitorService
{
    private readonly IMonitorRepository _repository = repository;

    public async Task<Monitor> CreateMonitorAsync(string name, string url, int intervalSeconds)
    {
        if (intervalSeconds < 10)
            throw new ArgumentException("O intervalo mínimo permitido é de 10 segundos.");

        var newMonitor = new Monitor(name, url, intervalSeconds);

        await _repository.AddAsync(newMonitor);
        return newMonitor;
    }

    public async Task<IEnumerable<Monitor>> GetAllMonitorsAsync()
    {
        return await _repository.GetAllAsync();
    }
}