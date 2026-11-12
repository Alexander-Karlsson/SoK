namespace Bookings.Application.Bookings.DTOs.Response;

public record BookingServiceResponse(
    Guid ServiceId,
    decimal EstimatedHours,
    decimal PricePerHour
    );
