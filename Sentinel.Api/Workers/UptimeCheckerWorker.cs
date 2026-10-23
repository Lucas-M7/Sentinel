using System.Diagnostics;
using Sentinel.Api.Entities;
using Sentinel.Api.Repositories.Interfaces;

namespace Sentinel.Api.Workers;

public class UptimeCheckerWorker(
    ILogger<UptimeCheckerWorker> logger,
    IServiceScopeFactory scopeFactory,
    IHttpClientFactory httpClientFactory) : BackgroundService
{
    private readonly ILogger<UptimeCheckerWorker> _logger = logger;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Iniciando ciclo de verificação...");

            using (var scope = _scopeFactory.CreateScope())
            {
                var monitorRepository = scope.ServiceProvider.GetRequiredService<IMonitorRepository>();
                var logRepository = scope.ServiceProvider.GetRequiredService<ICheckLogRepository>();

                var monitorsToCheck = await monitorRepository.GetMonitorsReadyForCheckAsync(DateTimeOffset.UtcNow);

                foreach (var monitor in monitorsToCheck)
                {
                    _logger.LogInformation("🔍 Verificando site: {Nome} ({Url})", monitor.Name, monitor.Url);

                    var client = _httpClientFactory.CreateClient();
                    var stopWatch = Stopwatch.StartNew();

                    try
                    {
                        var response = await client.GetAsync(monitor.Url, stoppingToken);
                        stopWatch.Stop();

                        var statusCode = (int)response.StatusCode;
                        var responseTime = (int)stopWatch.ElapsedMilliseconds;
                        var isSuccess = response.IsSuccessStatusCode;
                        var responseMessage = !string.IsNullOrWhiteSpace(response.ReasonPhrase)
                            ? response.ReasonPhrase
                            : (isSuccess ? "OK" : $"HTTP Status {statusCode}");

                        if (isSuccess)
                        {
                            _logger.LogInformation("🟢 [SUCESSO] {Nome} -> Status: {StatusCode} | Latência: {Tempo}ms", monitor.Name, statusCode, responseTime);
                        }
                        else
                        {
                            _logger.LogWarning("🟡 [ALERTA] {Nome} retornou status HTTP {StatusCode} em {Tempo}ms", monitor.Name, statusCode, responseTime);
                        }

                        var log = new CheckLog(
                            monitor.Id,
                            statusCode,
                            responseTime,
                            isSuccess,
                            responseMessage,
                            DateTimeOffset.UtcNow
                        );

                        await logRepository.AddAsync(log);
                    }
                    catch (Exception ex)
                    {
                        stopWatch.Stop();
                        var responseTime = (int)stopWatch.ElapsedMilliseconds;

                        _logger.LogError("🔴 [FALHA DE REDE] {Nome} -> Erro: {Erro}", monitor.Name, ex.Message);
                        var logErro = new CheckLog(
                            monitor.Id,
                            0,
                            responseTime,
                            false,
                            ex.Message,
                            DateTimeOffset.UtcNow
                        );

                        await logRepository.AddAsync(logErro);
                    }

                    monitor.ScheduleNextCheck(DateTimeOffset.UtcNow);
                    await monitorRepository.UpdateAsync(monitor);
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}