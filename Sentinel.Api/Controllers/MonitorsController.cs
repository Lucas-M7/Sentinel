using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sentinel.Api.Application.Monitors.Commands;
using Sentinel.Api.Application.Monitors.Queries;
using Sentinel.Api.DTOs.Monitor;

namespace Sentinel.Api.Controllers;

[ApiController]
[Route("api/monitors")]
public class MonitorsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MonitorResponse>>> GetAll(CancellationToken ct)
        => Ok(await sender.Send(new GetMonitorsQuery(), ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MonitorResponse>> GetById(int id, CancellationToken ct)
        => Ok(await sender.Send(new GetMonitorByIdQuery(id), ct));

    [HttpPost]
    public async Task<ActionResult<MonitorResponse>> Create(CreateMonitorRequest request, CancellationToken ct)
    {
        var result = await sender.Send(
            new CreateMonitorCommand(request.Name, request.Url, request.IntervalSeconds), ct);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MonitorResponse>> Update(int id, UpdateMonitorRequest request, CancellationToken ct)
        => Ok(await sender.Send(
            new UpdateMonitorCommand(id, request.Name, request.Url, request.IntervalSeconds), ct));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await sender.Send(new DeleteMonitorCommand(id), ct);
        return NoContent();
    }

    [HttpGet("{id:int}/logs")]
    public async Task<ActionResult<IReadOnlyList<CheckLogResponse>>> GetLogs(int id, CancellationToken ct)
        => Ok(await sender.Send(new GetMonitorLogsQuery(id), ct));
}