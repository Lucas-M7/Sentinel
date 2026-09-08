namespace Sentinel.Api.Entities;

public class CheckLog
{
    public int Id { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public int StatusCode { get; private set; }
    public int ResponseTimeMs { get; private set; }
    public bool IsSuccess { get; private set; }
    public string Response { get; private set; } = string.Empty;
    public int MonitorId { get; private set; }

    public Monitor Monitor { get; private set; } = null!;

    protected CheckLog () { }

    public CheckLog(int monitorId, int statusCode, int responseTimeMs, bool isSuccess, string response)
    {
        MonitorId = monitorId;
        StatusCode = statusCode;
        ResponseTimeMs = responseTimeMs;
        IsSuccess = isSuccess;
        Response = response ?? string.Empty;
        CreatedAt = DateTime.UtcNow;
    }
}