namespace Bookings.Application.Bookings.DTOs.Request;

public class UpdateBookingRequest
{
    public Guid? ProjectId { get; set; }
    public required DateTime StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
}