using MediatR;
using Sentinel.Api.Application.Common;
using Sentinel.Api.Application.Common.Exceptions;
using Sentinel.Api.Repositories.Interfaces;

namespace Sentinel.Api.Application.Monitors.Commands;

public record DeleteMonitorCommand(int Id) : IRequest;

public class DeleteMonitorCommandHandler(
    IMonitorRepository repository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteMonitorCommand>
{
    public async Task Handle(DeleteMonitorCommand command, CancellationToken ct)
    {
        var monitor = await repository.GetByIdAsync(command.Id, ct)
            ?? throw new NotFoundException("Monitor", command.Id);

        repository.Remove(monitor);
        await unitOfWork.SaveChangesAsync(ct);
    }
}