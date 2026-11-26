namespace Bookings.Application.Bookings.DTOs.Response;

public record ServiceTimeResponse(
    Guid ServiceId,
    string ServiceName,
    Guid? EmployeeId,
    decimal EstimatedHours,
    decimal ReportedHours,
    IReadOnlyList<TimeEntryResponse> Entries
    );