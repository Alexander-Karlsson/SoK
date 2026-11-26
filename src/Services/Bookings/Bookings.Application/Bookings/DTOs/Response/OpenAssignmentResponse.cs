namespace Bookings.Application.Bookings.DTOs.Response;

public record OpenAssignmentResponse(
    Guid BookingId,
    Guid ServiceId,
    string ServiceName,
    decimal EstimatedHours,
    DateTime StartsAt,
    DateTime EndsAt
    );