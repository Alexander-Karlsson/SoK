using Bookings.Application.Bookings.DTOs;
using Bookings.Domain.Entities;

namespace Bookings.Application.Bookings.Interfaces;

public interface IBookingRepository
{
    
    Task<Booking?> GetByIdAsync(Guid id);
    Task<Booking> AddAsync(BookingDetails details);
    Task UpdateAsync(Booking booking);
    Task DeleteAsync(Booking booking);
    Task<IReadOnlyList<Booking>> GetAllAsync(Guid? customerId = null);
    Task<BookingDetails?> GetBookingDetailsAsync(Guid id);
    Task<bool> IsAnyEmployeeBookedAsync(List<Guid> employeeIds, DateTime startsAt, DateTime endsAt, Guid? excludeBookingId = null);
}
