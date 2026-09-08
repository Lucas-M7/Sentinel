namespace Sentinel.Api.Entities;

public class Monitor
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public int IntervalSeconds { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    public ICollection<CheckLog> CheckLogs { get; private set; } = new List<CheckLog>();

    protected Monitor() { }

    public Monitor(string name, string url, int intervalSeconds)
    {
        Name = name;
        Url = url;
        IntervalSeconds = intervalSeconds;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string name, string url, int intervalSeconds)
    {
        Name = name;
        Url = url;
        IntervalSeconds = intervalSeconds;
    }

    public void Pause() => IsActive = false;
    public void Active() => IsActive = true;
}