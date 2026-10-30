using Employees.Domain.Enums;

namespace Employees.Domain.Entites;

public class Employee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Department Department { get; set; } = Department.IT;
    public EmployeeType EmployeeType { get; set; } = EmployeeType.Permanent;
    public Guid UserId { get; set; } = Guid.NewGuid();
    public int EmployeeNumber { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PersonalNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime EmploymentDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public int VacationDaysPerYear { get; set; }
    public bool IsActive { get; set; }

}
