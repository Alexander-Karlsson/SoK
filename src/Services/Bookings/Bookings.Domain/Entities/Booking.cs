using Bookings.Domain.Enums;

namespace Bookings.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required DateTime CreatedAt { get; set; } 
    public required DateTime StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public Guid ProjectId { get; set; }
    public Guid? InvoiceId { get; set; }
}
