using Sentinel.Api.Entities;

namespace Sentinel.Api.Repositories.Interfaces;

public interface ICheckLogRepository
{
    Task AddAsync(CheckLog checkLog);
}