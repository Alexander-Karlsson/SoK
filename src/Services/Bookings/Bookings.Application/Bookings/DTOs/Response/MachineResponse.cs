namespace Bookings.Application.Bookings.DTOs.Response;

public record MachineResponse(
    Guid Id,
    string Name,
    string Type,
    string RegistrationNumber,
    decimal PricePerDay,
    bool IsOutOfService
    );

