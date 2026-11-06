namespace Bookings.Application.Bookings.DTOs;

public class CreateBookingMachineRequest
{
    public required Guid MachineId { get; set; }
    public required decimal PricePerDay { get; set; }
}