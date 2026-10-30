namespace Bookings.Domain.Entities;

public class BookingService
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BookingId { get; set; }
    public Guid ServiceId { get; set; }
    public decimal EstimatedHours { get; set; }
    public decimal PricePerHour { get; set; }
}
