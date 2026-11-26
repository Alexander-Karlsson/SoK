using Bookings.Domain.Enums;

namespace Bookings.Application.Bookings.DTOs.Response;

public record MyAssignmentResponse(
    Guid BookingId,
    DateTime StartsAt,
    DateTime EndsAt,
    BookingStatus Status,
    Guid? ServiceId,
    string? ServiceName,
    decimal? EstimatedHours
    );