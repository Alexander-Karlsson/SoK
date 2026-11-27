namespace Bookings.Application.Bookings.DTOs.Response;

public record BusinessServiceResponse(
    Guid Id,
    string Name,
    string? Description,
    decimal PricePerHour,
    bool IsActive,
    string? RequiredTitle
    );