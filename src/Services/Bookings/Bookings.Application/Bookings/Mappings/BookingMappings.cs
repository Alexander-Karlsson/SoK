using Bookings.Application.Bookings.DTOs;
using Bookings.Application.Bookings.DTOs.Response;
using Bookings.Domain.Entities;

namespace Bookings.Application.Bookings.Mappings;

public static class BookingMappings
{
    public static BookingResponse ToResponse(this BookingDetails details) => new(
        details.Booking.Id,
        details.Booking.CreatedAt,
        details.Booking.StartsAt,
        details.Booking.EndsAt,
        details.Booking.Status,
        details.Booking.CustomerId,
        details.Booking.ProjectId,
        details.Booking.InvoiceId,
        details.Employees.Select(e => e.EmployeeId).ToList(),
        details.Services.Select(s => new BookingServiceResponse(s.ServiceId, s.EstimatedHours, s.PricePerHour)).ToList(),
        details.Machines.Select(m => new BookingMachineResponse(m.MachineId, m.PricePerDay)).ToList()
        );

    public static BookingSummaryResponse ToSummary(this Booking booking) => new(
        booking.Id,
        booking.StartsAt,
        booking.EndsAt,
        booking.Status,
        booking.CustomerId,
        booking.ProjectId
        );
}
