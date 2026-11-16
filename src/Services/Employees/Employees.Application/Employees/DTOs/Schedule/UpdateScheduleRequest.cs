namespace Employees.Application.Employees.DTOs.Schedule;

public class UpdateScheduleRequest
{
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public int BreakMinutes { get; set; }
}
