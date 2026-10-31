namespace Sentinel.Api.DTOs.Monitor;

public record MonitorResponse(
    int Id, string Name, string Url, bool IsActive,
    int IntervalSeconds, DateTimeOffset CreatedAt, DateTimeOffset NextCheckAt);