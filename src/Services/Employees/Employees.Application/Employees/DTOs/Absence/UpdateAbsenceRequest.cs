using Employees.Domain.Enums;

namespace Employees.Application.Employees.DTOs.Absence;

public class UpdateAbsenceRequest
{
    public required AbsenceType Type { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
}
