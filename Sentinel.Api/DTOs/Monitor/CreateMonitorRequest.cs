namespace Sentinel.Api.DTOs.Monitor;

public record CreateMonitorRequest(string Name, string Url, int IntervalSeconds);