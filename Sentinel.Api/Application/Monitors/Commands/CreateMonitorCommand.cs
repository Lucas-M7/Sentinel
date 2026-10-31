// Application/Monitors/Commands/CreateMonitorCommand.cs
using MediatR;
using Sentinel.Api.Application.Common;
using Sentinel.Api.DTOs.Monitor;
using Sentinel.Api.Repositories.Interfaces;
using Monitor = Sentinel.Api.Entities.Monitor;

namespace Sentinel.Api.Application.Monitors.Commands;

public record CreateMonitorCommand(string Name, string Url, int IntervalSeconds)
    : IRequest<MonitorResponse>;

public class CreateMonitorCommandHandler(
    IMonitorRepository repository,
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider) : IRequestHandler<CreateMonitorCommand, MonitorResponse>
{
    public async Task<MonitorResponse> Handle(CreateMonitorCommand command, CancellationToken ct)
    {
        var monitor = new Monitor(command.Name, command.Url, command.IntervalSeconds, timeProvider.GetUtcNow());

        repository.Add(monitor);
        await unitOfWork.SaveChangesAsync(ct);

        return monitor.ToResponse();
    }
}