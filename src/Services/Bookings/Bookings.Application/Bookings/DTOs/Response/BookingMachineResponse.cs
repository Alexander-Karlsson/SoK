namespace Bookings.Application.Bookings.DTOs.Response;

public record BookingMachineResponse(
    Guid MachineId,
    string Name,
    decimal PricePerDay
    );
