namespace Bookings.Application.Bookings.DTOs.Request;

public class CreateBookingServiceRequest
{
    public required Guid ServiceId { get; set; }
    public required decimal EstimatedHours { get; set; }
    public required decimal PricePerHour { get; set; }
}