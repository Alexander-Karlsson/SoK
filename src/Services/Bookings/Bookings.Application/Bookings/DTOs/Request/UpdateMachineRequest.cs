namespace Bookings.Application.Bookings.DTOs.Request;

public class UpdateMachineRequest
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string RegistrationNumber { get; set; }
    public required decimal PricePerDay { get; set; }
    public required bool IsOutOfService { get; set; }

}
