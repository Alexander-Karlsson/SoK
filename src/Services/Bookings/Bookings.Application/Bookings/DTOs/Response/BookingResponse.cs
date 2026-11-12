using Bookings.Domain.Enums;

namespace Bookings.Application.Bookings.DTOs.Response;

public record BookingResponse(
    Guid Id,
    DateTime CreatedAt,
    DateTime StartsAt,
    DateTime? EndsAt,
    BookingStatus Status,
    Guid CustomerId,
    Guid? ProjectId,
    Guid? InvoiceId,
    IReadOnlyList<Guid> EmployeeIds,
    IReadOnlyList<BookingServiceResponse> Service,
    IReadOnlyList<BookingMachineResponse> Machines
    );

