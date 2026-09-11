using Monitor = Sentinel.Api.Entities.Monitor;

namespace Sentinel.Api.Services.Interfaces;

public interface IMonitorService
{
    Task<IEnumerable<Monitor>> GetAllMonitorsAsync();
    Task<Monitor> CreateMonitorAsync(string name, string url, int intervalSeconds);
}