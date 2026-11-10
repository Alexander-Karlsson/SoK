using Employees.Application.Employees.DTOs.Employee;
using Employees.Application.Employees.Services;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(EmployeeService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest request)
    {
        var employee = await service.CreateEmployeeAsync(request);
        return Ok(employee);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEmployeeById(Guid id)
    {
        var employee = await service.GetEmployeeByIdAsync(id);
        
        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] CreateEmployeeRequest request)
    {
        var employee = await service.UpdateEmployeeAsync(id, request);
        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        await service.DeleteEmployeeAsync(id);
        return NoContent(); // returnerar 204 som är den mer korrekta koden enligt HTTP-konventionen iställer för Ok() som är 200
    }
}
