namespace Bookings.Application.Bookings.DTOs.Response;

public record BookingServiceResponse(
    Guid ServiceId,
    string Name,
    decimal EstimatedHours,
    decimal PricePerHour,
    Guid? EmployeeId
    );
