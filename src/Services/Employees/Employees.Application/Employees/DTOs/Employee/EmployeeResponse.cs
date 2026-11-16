using Employees.Domain.Enums;

namespace Employees.Application.Employees.DTOs.Employee;

public class EmployeeResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
    public Department Department { get; set; }
    public EmployeeType EmployeeType { get; set; }
    public DateOnly EmploymentStartDate { get; set; }
    public DateOnly? EmploymentEndDate { get; set; }
    public decimal EmployeeRate { get; set; }
    public int VacationDaysPerYear { get; set; }
}
