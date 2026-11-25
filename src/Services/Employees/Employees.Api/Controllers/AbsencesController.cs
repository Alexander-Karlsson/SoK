using Employees.Application.Employees.DTOs;
using Employees.Application.Employees.DTOs.Absence;
using Employees.Application.Employees.Services;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Api.Controllers;

[ApiController]
[Route("api/employees/{employeeId:guid}/absences")]
public class AbsencesController(AbsenceService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAbsences(Guid employeeId)
    {
        var absences = await service.GetAbsencesByEmployeeIdAsync(employeeId);
        return Ok(absences);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAbsenceById(Guid employeeId, Guid id)
    {
        var absence = await service.GetAbsenceByIdAsync(employeeId, id);

        if (absence is null)
        {
            return NotFound();
        }

        return Ok(absence);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAbsence(Guid employeeId, [FromBody] CreateAbsenceRequest request)
    {
        var (result, absence) = await service.CreateAbsenceAsync(employeeId, request);

        if (result == AbsenceResult.EmployeeNotFound)
        {
            return NotFound();
        }

        if (result == AbsenceResult.InvalidDates)
        {
            return BadRequest(new ErrorResponse("Slutdatum kan inte vara före startdatum."));
        }

        if (result == AbsenceResult.Overlapping)
        {
            return Conflict(new ErrorResponse("Den anställda har redan frånvaro under den perioden."));
        }

        return CreatedAtAction(nameof(GetAbsenceById), new { employeeId, id = absence!.Id }, absence);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAbsence(Guid employeeId, Guid id, [FromBody] UpdateAbsenceRequest request)
    {
        var (result, absence) = await service.UpdateAbsenceAsync(employeeId, id, request);

        if (result == AbsenceResult.AbsenceNotFound)
        {
            return NotFound();
        }

        if (result == AbsenceResult.InvalidDates)
        {
            return BadRequest(new ErrorResponse("Slutdatum kan inte vara före startdatum."));
        }

        if (result == AbsenceResult.Overlapping)
        {
            return Conflict(new ErrorResponse("Den anställda har redan frånvaro under den perioden."));
        }

        return Ok(absence);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAbsence(Guid employeeId, Guid id)
    {
        var deleted = await service.DeleteAbsenceAsync(employeeId, id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}