namespace Bookings.Application.Bookings.DTOs.Response;

public record TimeEntryResponse(
    Guid Id,
    Guid BookingServiceId,
    Guid EmployeeId,
    DateOnly WorkDate,
    decimal Hours,
    string? Note,
    DateTime CreatedAt
    );