using Employees.Application.Employees.DTOs.Schedule;
using Employees.Application.Employees.Services;
using Employees.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulesController(ScheduleService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleRequest request)
    {
        var schedule = await service.CreateScheduleAsync(request);
        return Ok(schedule);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetScheduleById(Guid id)
    {
        var schedule = await service.GetScheduleByIdAsync(id);

        if (schedule == null)
        {
            return NotFound();
        }

        return Ok(schedule);
    }

    [HttpGet]
    public async Task<IActionResult> GetSchedulesByEmployee(
    [FromQuery] Guid employeeId)
    {
        var schedules = await service.GetSchedulesByEmployeeIdAsync(employeeId);
        return Ok(schedules);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSchedule(Guid id, [FromBody] UpdateScheduleRequest request)
    {
        var schedule = await service.UpdateScheduleAsync(id, request);

        if (schedule == null)
        {
            return NotFound();
        }

        return Ok(schedule);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSchedule(Guid id)
    {
        await service.DeleteScheduleAsync(id);
        return NoContent();
    }
}