using Microsoft.AspNetCore.Mvc;
using Sentinel.Api.DTOs.Monitor;
using Sentinel.Api.Services.Interfaces;

namespace Sentinel.Api.Controllers;

[ApiController]
[Route("api/monitors")]
public class MonitorsController(IMonitorService service) : ControllerBase
{
    private readonly IMonitorService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAllMonitors()
    {
        var result = await _service.GetAllMonitorsAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMonitor([FromBody] CreateMonitorRequest request)
    {
        try
        {
            var result = await _service.CreateMonitorAsync(request.Name, request.Url, request.IntervalSeconds);
            return Created(string.Empty, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Ocorreu um erro interno no servidor."});
        }
    }
}