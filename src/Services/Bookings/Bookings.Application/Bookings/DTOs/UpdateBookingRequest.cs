namespace Bookings.Application.Bookings.DTOs;

public class UpdateBookingRequest
{
    public Guid? ProjectId { get; set; }
    public required DateTime StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
}