using Microsoft.EntityFrameworkCore;
using Sentinel.Api.Data;
using Sentinel.Api.Repositories.Interfaces;
using Monitor = Sentinel.Api.Entities.Monitor;

namespace Sentinel.Api.Repositories;

public class MonitorRepository(AppDbContext context) : IMonitorRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Monitor monitor)
    {
        await _context.Monitors.AddAsync(monitor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Monitor monitor)
    {
        _context.Monitors.Remove(monitor);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Monitor>> GetAllAsync()
    {
        return await _context.Monitors.AsNoTracking().ToListAsync();
    }

    public async Task<Monitor?> GetByIdAsync(int id)
    {
        return await _context.Monitors.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task UpdateAsync(Monitor monitor)
    {
        _context.Monitors.Update(monitor);
        await _context.SaveChangesAsync();
    }
}