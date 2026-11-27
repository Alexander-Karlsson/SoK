namespace Bookings.Domain.Entities;

public class TimeEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BookingServiceId { get; set; }
    public Guid EmployeeId { get; set; }
    public DateOnly WorkDate { get; set; }
    public decimal Hours { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
}