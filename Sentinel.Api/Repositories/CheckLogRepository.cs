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
}