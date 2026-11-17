using Employees.Application.Employees.DTOs.EmployeePayrol;
using Employees.Application.Employees.DTOs.EmployeePayroll;
using Employees.Application.Employees.Services;
using Employees.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeePayrollsController(EmployeePayrollService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePayroll([FromBody] CreateEmployeePayrollRequest request)
    {
        var payroll = await service.CreatePayrollAsync(request);
        return Ok(payroll);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPayrollById(Guid id)
    {
        var payroll = await service.GetPayrollByIdAsync(id);

        if (payroll == null)
        {
            return NotFound();
        }

        return Ok(payroll);
    }

    [HttpGet]
    public async Task<IActionResult> GetPayrollsByEmployee([FromQuery] Guid employeeId)
    {
        var payrolls = await service.GetPayrollsByEmployeeIdAsync(employeeId);
        return Ok(payrolls);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePayroll(Guid id, [FromBody] UpdateEmployeePayrollRequest request)
    {
        var payroll = await service.UpdatePayrollAsync(id, request);

        if (payroll == null)
        {
            return NotFound();
        }

        return Ok(payroll);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePayroll(Guid id)
    {
        await service.DeletePayrollAsync(id);
        return NoContent();
    }
}