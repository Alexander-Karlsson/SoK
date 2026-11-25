using Employees.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Domain.Entities;

public class Absence
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EmployeeId { get; set; }
    public AbsenceType AbsenceType { get; set; } = AbsenceType.Sick;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string Reason { get; set; } = string.Empty;
}
