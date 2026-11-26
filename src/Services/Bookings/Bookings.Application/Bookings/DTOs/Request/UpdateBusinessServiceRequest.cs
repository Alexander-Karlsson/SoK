namespace Bookings.Application.Bookings.DTOs.Request;

public class UpdateBusinessServiceRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal PricePerHour { get; set; }
    public required bool IsActive { get; set; }
    public string? RequiredTitle { get; set; }
}