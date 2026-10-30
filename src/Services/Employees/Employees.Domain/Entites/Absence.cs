using Employees.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Domain.Entites;

public class Absence
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EmployeeId { get; set; }
    public AbsenceType AbsenceType { get; set; } = AbsenceType.Sick;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
}
