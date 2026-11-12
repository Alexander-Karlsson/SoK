namespace Bookings.Domain.Entities;

public class BusinessService
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal PricePerHour { get; set; }
    public bool IsActive { get; set; } = true;
}