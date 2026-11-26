using Employees.Application.Employees.DTOs;
using Employees.Application.Employees.DTOs.Payslip;
using Employees.Application.Employees.Services;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Api.Controllers;

[ApiController]
[Route("api/employees/{employeeId:guid}/payslips")]
public class PayslipsController(PayslipService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPayslips(Guid employeeId)
    {
        var payslips = await service.GetPayslipsAsync(employeeId);
        return Ok(payslips);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPayslipById(Guid employeeId, Guid id)
    {
        var payslip = await service.GetPayslipAsync(employeeId, id);

        if (payslip is null)
        {
            return NotFound();
        }

        return Ok(payslip);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayslip(Guid employeeId, [FromBody] CreatePayslipRequest request)
    {
        var (result, payslip) = await service.CreatePayslipAsync(employeeId, request);

        if (result == PayslipResult.EmployeeNotFound)
        {
            return NotFound();
        }

        if (result == PayslipResult.PayrollNotFound)
        {
            return BadRequest(new ErrorResponse("Den anställda saknar lönevillkor. Lägg till lön först."));
        }

        if (result == PayslipResult.AlreadyExists)
        {
            return Conflict(new ErrorResponse("Det finns redan en lönespecifikation för den månaden."));
        }

        return CreatedAtAction(nameof(GetPayslipById), new { employeeId, id = payslip!.Id }, payslip);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePayslip(Guid employeeId, Guid id)
    {
        var deleted = await service.DeletePayslipAsync(employeeId, id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}