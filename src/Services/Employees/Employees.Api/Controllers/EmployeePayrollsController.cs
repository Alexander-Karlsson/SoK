using Employees.Application.Employees.Services;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeePayrollsController(EmployeePayrollService service) : ControllerBase
{
}
