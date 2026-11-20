namespace Bookings.Domain.Entities;

public class BookingEmployee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BookingId { get; set; }
    public Guid EmployeeId { get; set; }
}
