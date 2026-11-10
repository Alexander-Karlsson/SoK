using Employees.Application.Employees.DTOs.Absence;
using Employees.Application.Employees.Services;
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
}
