namespace Bookings.Domain.Entities;

public class Machine
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string RegistrationNumber { get; set; }
    public decimal PricePerDay { get; set; }
    public bool IsOutOfService { get; set; }
}
