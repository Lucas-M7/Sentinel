using MediatR;
using Sentinel.Api.Application.Common.Exceptions;
using Sentinel.Api.DTOs.Monitor;
using Sentinel.Api.Repositories.Interfaces;

namespace Sentinel.Api.Application.Monitors.Queries;

public record GetMonitorByIdQuery(int Id) : IRequest<MonitorResponse>;

public class GetMonitorByIdQueryHandler(IMonitorRepository repository)
    : IRequestHandler<GetMonitorByIdQuery, MonitorResponse>
{
    public async Task<MonitorResponse> Handle(GetMonitorByIdQuery query, CancellationToken ct)
    {
        var monitor = await repository.GetByIdAsync(query.Id, ct)
            ?? throw new NotFoundException("Monitor", query.Id);

        return monitor.ToResponse();
    }
}