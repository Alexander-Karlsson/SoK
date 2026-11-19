namespace Bookings.Application.Bookings.DTOs.Request;

public class CreateMachineRequest
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string RegistrationNumber { get; set; }
    public required decimal PricePerDay { get; set; }
}
