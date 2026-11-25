using Employees.Domain.Enums;

namespace Employees.Application.Employees.DTOs.Absence;

public class AbsenceResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public AbsenceType Type { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly  EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
}
