namespace Bookings.Application.Bookings.DTOs.Request;

public class CompleteBookingRequest
{
    public required Guid TemplateId { get; set; }
    public DateTime? EndsAt { get; set; }
}