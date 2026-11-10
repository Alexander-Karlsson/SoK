using Employees.Domain.Enums;

namespace Employees.Application.Employees.DTOs.Employee;

public class CreateEmployeeRequest
{
    // Personuppgifter
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PersonalNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    // Nödkontaktuppgifter
    public string EmergencyContactName { get; set; } = string.Empty;
    public string EmergencyContactNumber { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;

    // Anställningsuppgifter
    public string Title { get; set; } = string.Empty;
    public Department Department { get; set; } 
    public EmployeeType EmployeeType { get; set; } 
    public DateTime EmploymentDate { get; set; }
    public int VacationDaysPerYear { get; set; } = 25;

    // Ta endast med denna om administratören manuellt ska bestämma numret
    // public int EmployeeNumber { get; set; }
}