namespace Sentinel.Api.DTOs.Monitor;

public record CheckLogResponse(
    int Id, bool IsSuccess, int StatusCode, int ResponseTimeMs,
    string Response, DateTimeOffset CreatedAt);