using Sentinel.Api.DTOs.Monitor;
using Sentinel.Api.Entities;
using Monitor = Sentinel.Api.Entities.Monitor;

namespace Sentinel.Api.Application.Monitors;

public static class MonitorMappings
{
    public static MonitorResponse ToResponse(this Monitor m) => new(
        m.Id, m.Name, m.Url, m.IsActive, m.IntervalSeconds, m.CreatedAt, m.NextCheckAt);

    public static CheckLogResponse ToResponse(this CheckLog l) => new(
        l.Id, l.IsSuccess, l.StatusCode, l.ResponseTimeMs, l.Response, l.CreatedAt);
}