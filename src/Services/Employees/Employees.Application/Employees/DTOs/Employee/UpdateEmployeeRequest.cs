using Employees.Domain.Enums;

namespace Employees.Application.Employees.DTOs.Employee;

public class UpdateEmployeeRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PersonalNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public string EmergencyContactName { get; set; } = string.Empty;
    public string EmergencyContactNumber { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
    public Department Department { get; set; }
    public EmployeeType EmployeeType { get; set; }
    public DateOnly EmploymentStartDate { get; set; }
    public int VacationDaysPerYear { get; set; } = 25;
}
