namespace Bookings.Domain.Entities;

public class BookingMachine
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BookingId { get; set; }
    public Guid MachineId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
}
