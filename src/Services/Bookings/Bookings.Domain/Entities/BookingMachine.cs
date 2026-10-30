namespace Bookings.Domain.Entities;

public class BookingMachine
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BookingId { get; set; }
    public Guid MachineId { get; set; }
    public decimal PricePerDay { get; set; }
}
