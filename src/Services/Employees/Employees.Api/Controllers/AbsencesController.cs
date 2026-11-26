using Employees.Application.Employees.DTOs.Absence;
using Employees.Application.Employees.Services;
using Employees.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AbsencesController(AbsenceService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAbsence([FromBody] CreateAbsenceRequest request)
    {
        var absence = await service.CreateAbsenceAsync(request);
        return Ok(absence);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAbsenceById(Guid id)
    {
        var absence = await service.GetAbsenceByIdAsync(id);

        if (absence == null)
        {
            return NotFound();
        }

        return Ok(absence);
    }

    [HttpGet]
    public async Task<IActionResult> GetAbsencesByEmployee([FromQuery] Guid employeeId)
    {
        var absences = await service.GetAbsencesByEmployeeIdAsync(employeeId);
        return Ok(absences);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAbsence(Guid id, [FromBody] UpdateAbsenceRequest request)
    {
        var absence = await service.UpdateAbsenceAsync(id, request);

        if (absence == null)
        {
            return NotFound();
        }

        return Ok(absence);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAbsence(Guid id)
    {
        await service.DeleteAbsenceAsync(id);
        return NoContent();
    }
}