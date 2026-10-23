namespace Sentinel.Api.Entities;

public class CheckLog
{
    public const int ResponseMaxLength = 500;

    public int Id { get; private set; }
    public int MonitorId { get; private set; }
    public bool IsSuccess { get; private set; }
    public int StatusCode { get; private set; }
    public int ResponseTimeMs { get; private set; }
    public string Response { get; private set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private set; }

    public Monitor Monitor { get; private set; } = null!;

    protected CheckLog() { }

    public CheckLog(int monitorId, int statusCode, int responseTimeMs,
        bool isSuccess, string? response, DateTimeOffset now)
    {
        MonitorId = monitorId;
        StatusCode = statusCode;
        ResponseTimeMs = responseTimeMs;
        IsSuccess = isSuccess;
        Response = Truncate(response);
        CreatedAt = now;
    }

    private static string Truncate(string? value) =>
        string.IsNullOrEmpty(value) ? string.Empty
        : value.Length <= ResponseMaxLength ? value
        : value[..ResponseMaxLength];
}