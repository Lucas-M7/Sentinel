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
                var repository = scope.ServiceProvider.GetRequiredService<IMonitorRepository>();
                var logRepository = scope.ServiceProvider.GetRequiredService<ICheckLogRepository>();

                var allMonitors = await repository.GetAllAsync();
                var activeMonitors = allMonitors.Where(m => m.IsActive);

                foreach (var monitor in activeMonitors)
                {
                    _logger.LogInformation("Verificando site: {Nome} - {Url}", monitor.Name, monitor.Url);

                    var client = _httpClientFactory.CreateClient();
                    var stopWatch = Stopwatch.StartNew();

                    try
                    {
                        var response = await client.GetAsync(monitor.Url, stoppingToken);
                        stopWatch.Stop();

                        var statusCode = (int)response.StatusCode;
                        var responseTime = (int)stopWatch.ElapsedMilliseconds;
                        var isSuccess = response.IsSuccessStatusCode;

                        _logger.LogInformation
                            ("Site {Nome} retornou status {StatusCode} em {Tempo}ms", monitor.Name, statusCode, responseTime);

                        var log = new CheckLog(
                            monitor.Id,
                            statusCode,
                            responseTime,
                            isSuccess,
                            "OK"
                        );

                        await logRepository.AddAsync(log);
                    }
                    catch (Exception ex)
                    {
                        stopWatch.Stop();
                        var responseTime = (int)stopWatch.ElapsedMilliseconds;

                        _logger.LogError("Erro ao verificar site {Nome}: {Erro}", monitor.Name, ex.Message);

                        var logErro = new CheckLog(
                            monitor.Id,
                            0,
                            responseTime,
                            false,
                            ex.Message
                        );

                        await logRepository.AddAsync(logErro);
                    }
                }
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}