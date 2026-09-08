using Microsoft.EntityFrameworkCore;
using Sentinel.Api.Entities;
using Monitor = Sentinel.Api.Entities.Monitor;

namespace Sentinel.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Monitor> Monitors => Set<Monitor>();
    public DbSet<CheckLog> CheckLogs => Set<CheckLog>();
}