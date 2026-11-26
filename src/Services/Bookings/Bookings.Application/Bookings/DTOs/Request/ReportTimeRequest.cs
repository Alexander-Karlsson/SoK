namespace Bookings.Application.Bookings.DTOs.Request;

public class ReportTimeRequest
{
    public required Guid EmployeeId { get; set; }
    public required DateOnly WorkDate { get; set; }
    public required decimal Hours { get; set; }
    public string? Note { get; set; }
}