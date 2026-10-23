namespace Sentinel.Api.Entities;

public class Monitor
{
    public const int MinIntervalSeconds = 10;
    public const int NameMaxLength = 100;
    public const int UrlMaxLength = 2048;

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Url { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public int IntervalSeconds { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset NextCheckAt { get; private set; }

    public ICollection<CheckLog> CheckLogs { get; private set; } = [];

    protected Monitor() { }

    public Monitor(string name, string url, int intervalSeconds, DateTimeOffset now)
    {
        Apply(name, url, intervalSeconds);
        IsActive = true;
        CreatedAt = now;
        NextCheckAt = now;
    }

    public void UpdateDetails(string name, string url, int intervalSeconds)
        => Apply(name, url, intervalSeconds);

    public void ScheduleNextCheck(DateTimeOffset now)
        => NextCheckAt = now.AddSeconds(IntervalSeconds);

    public void Pause() => IsActive = false;

    public void Activate(DateTimeOffset now)
    {
        IsActive = true;
        NextCheckAt = now;
    }

    private void Apply(string name, string url, int intervalSeconds)
    {
        Name = name.Trim();
        Url = url.Trim();
        IntervalSeconds = intervalSeconds;
        
        if (string.IsNullOrWhiteSpace(name) || name.Length > NameMaxLength)
            throw new DomainException($"O nome é obrigatório e deve ter até {NameMaxLength} caracteres.");

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri)
            || uri.Scheme is not ("http" or "https")
            || url.Length > UrlMaxLength)
            throw new DomainException("A URL deve ser absoluta, http ou https.");

        if (intervalSeconds < MinIntervalSeconds)
            throw new DomainException($"O intervalo mínimo permitido é de {MinIntervalSeconds} segundos.");
    }
}