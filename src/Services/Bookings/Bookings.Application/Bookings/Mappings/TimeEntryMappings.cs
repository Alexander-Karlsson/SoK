using Bookings.Application.Bookings.DTOs.Response;
using Bookings.Domain.Entities;

namespace Bookings.Application.Bookings.Mappings;

public static class TimeEntryMappings
{
    public static TimeEntryResponse ToResponse(this TimeEntry entry) => new(
        entry.Id,
        entry.BookingServiceId,
        entry.EmployeeId,
        entry.WorkDate,
        entry.Hours,
        entry.Note,
        entry.CreatedAt
        );
}