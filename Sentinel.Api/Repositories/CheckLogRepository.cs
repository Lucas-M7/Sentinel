using Microsoft.EntityFrameworkCore;
using Sentinel.Api.Data;
using Sentinel.Api.Entities;
using Sentinel.Api.Repositories.Interfaces;

namespace Sentinel.Api.Repositories;

public class CheckLogRepository(AppDbContext context) : ICheckLogRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(CheckLog checkLog)
    {
        await _context.AddAsync(checkLog);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CheckLog>> GetLogsByMonitorIdAsync(int monitorId, int limit = 50)
    {
        return await _context.CheckLogs.Where(x => x.MonitorId == monitorId)
            .OrderByDescending(x => x.CreatedAt).Take(limit).ToListAsync();
    }
}