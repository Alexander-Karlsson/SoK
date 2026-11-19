using Employees.Domain.Enums;

namespace Employees.Application.Employees.DTOs.Absence;

public class CreateAbsenceRequest
{
    public required Guid EmployeeId { get; set; }
    public required AbsenceType Type { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
}
