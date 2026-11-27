using Bookings.Domain.Entities;

namespace Bookings.Application.Bookings.DTOs;

public record BookingDetails(
    Booking Booking,
    IReadOnlyList<BookingEmployee> Employees,
    IReadOnlyList<BookingService> Services,
    IReadOnlyList<BookingMachine> Machines);

