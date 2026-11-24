using Employees.Application.Employees.DTOs.EmployeePayroll;
using Employees.Application.Employees.Services;
using Employees.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Api.Controllers;

[ApiController]
[Route("api/employees/{employeeId:guid}/payroll")]
public class EmployeePayrollsController(EmployeePayrollService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePayroll(Guid employeeId, [FromBody] CreateEmployeePayrollRequest request)
    {
        var (result, payroll) = await service.CreatePayrollAsync(employeeId, request);

        if (result == PayrollResult.EmployeeNotFound)
        {
            return NotFound();
        }

        if (result == PayrollResult.AlreadyExists)
        {
            return Conflict("Den anställda har redan lönevillkor.");
        }

        return CreatedAtAction(nameof(GetPayroll), new { employeeId }, payroll);
    }

    [HttpGet]
    public async Task<IActionResult> GetPayroll(Guid employeeId)
    {
        var payroll = await service.GetPayrollAsync(employeeId);

        if (payroll is null)
        {
            return NotFound();
        }

        return Ok(payroll);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePayroll(Guid employeeId, [FromBody] UpdateEmployeePayrollRequest request)
    {
        var payroll = await service.UpdatePayrollAsync(employeeId, request);

        if (payroll is null)
        {
            return NotFound();
        }

        return Ok(payroll);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePayroll(Guid employeeId)
    {
        var deleted = await service.DeletePayrollAsync(employeeId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}