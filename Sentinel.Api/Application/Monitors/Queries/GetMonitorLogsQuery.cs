using MediatR;
using Sentinel.Api.Application.Common.Exceptions;
using Sentinel.Api.DTOs.Monitor;
using Sentinel.Api.Repositories.Interfaces;

namespace Sentinel.Api.Application.Monitors.Queries;

public record GetMonitorLogsQuery(int MonitorId, int Limit = 50)
    : IRequest<IReadOnlyList<CheckLogResponse>>;

public class GetMonitorLogsQueryHandler(
    IMonitorRepository monitorRepository,
    ICheckLogRepository logRepository)
    : IRequestHandler<GetMonitorLogsQuery, IReadOnlyList<CheckLogResponse>>
{
    public async Task<IReadOnlyList<CheckLogResponse>> Handle(GetMonitorLogsQuery query, CancellationToken ct)
    {
        if (!await monitorRepository.ExistsAsync(query.MonitorId, ct))
            throw new NotFoundException("Monitor", query.MonitorId);

        var logs = await logRepository.GetLogsByMonitorIdAsync(query.MonitorId, query.Limit, ct);
        return logs.Select(l => l.ToResponse()).ToList();
    }
}