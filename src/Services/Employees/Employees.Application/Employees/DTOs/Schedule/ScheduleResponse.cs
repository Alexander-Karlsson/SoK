namespace Employees.Application.Employees.DTOs.Schedule;

public class ScheduleResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int BreakMinutes { get; set; }
}
