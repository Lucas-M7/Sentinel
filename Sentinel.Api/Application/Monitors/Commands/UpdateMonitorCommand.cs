using MediatR;
using Sentinel.Api.Application.Common;
using Sentinel.Api.Application.Common.Exceptions;
using Sentinel.Api.DTOs.Monitor;
using Sentinel.Api.Repositories.Interfaces;

namespace Sentinel.Api.Application.Monitors.Commands;

public record UpdateMonitorCommand(int Id, string Name, string Url, int IntervalSeconds)
    : IRequest<MonitorResponse>;

public class UpdateMonitorCommandHandler(
    IMonitorRepository repository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateMonitorCommand, MonitorResponse>
{
    public async Task<MonitorResponse> Handle(UpdateMonitorCommand command, CancellationToken ct)
    {
        var monitor = await repository.GetByIdAsync(command.Id, ct)
            ?? throw new NotFoundException("Monitor", command.Id);

        monitor.UpdateDetails(command.Name, command.Url, command.IntervalSeconds);
        await unitOfWork.SaveChangesAsync(ct);

        return monitor.ToResponse();
    }
}