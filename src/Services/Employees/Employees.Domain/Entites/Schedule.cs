using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Domain.Entites;

public class Schedule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EmployeeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal BreakMinutes { get; set; }

}
