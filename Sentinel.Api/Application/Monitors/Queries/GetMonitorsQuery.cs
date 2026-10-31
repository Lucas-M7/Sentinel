using MediatR;
using Sentinel.Api.DTOs.Monitor;
using Sentinel.Api.Repositories.Interfaces;

namespace Sentinel.Api.Application.Monitors.Queries;

public record GetMonitorsQuery : IRequest<IReadOnlyList<MonitorResponse>>;

public class GetMonitorsQueryHandler(IMonitorRepository repository)
    : IRequestHandler<GetMonitorsQuery, IReadOnlyList<MonitorResponse>>
{
    public async Task<IReadOnlyList<MonitorResponse>> Handle(GetMonitorsQuery query, CancellationToken ct)
    {
        var monitors = await repository.GetAllAsync(ct);
        return monitors.Select(m => m.ToResponse()).ToList();
    }
}