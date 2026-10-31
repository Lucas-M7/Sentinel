namespace Sentinel.Api.DTOs.Monitor;

public record UpdateMonitorRequest(string Name, string Url, int IntervalSeconds);