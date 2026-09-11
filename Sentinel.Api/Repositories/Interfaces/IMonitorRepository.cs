using Monitor = Sentinel.Api.Entities.Monitor;

namespace Sentinel.Api.Repositories.Interfaces;

public interface IMonitorRepository
{
    Task<IEnumerable<Monitor>> GetAllAsync();
    Task<Monitor?> GetByIdAsync(int id);
    Task AddAsync(Monitor monitor);
    Task UpdateAsync(Monitor monitor);
    Task DeleteAsync(Monitor monitor);
}