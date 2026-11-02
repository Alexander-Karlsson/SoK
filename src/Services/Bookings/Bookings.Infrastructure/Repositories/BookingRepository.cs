using Bookings.Application.Bookings.Interfaces;
using Bookings.Domain.Entities;
using Bookings.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Repositories;

public class BookingRepository(BookingDbContext context) : IBookingRepository
{
    public async Task<Booking> AddAsync(Booking booking)
    {
        await context.Bookings.AddAsync(booking);
        await context.SaveChangesAsync();
        return booking;
    }

    public async Task DeleteAsync(Booking booking)
    {
        context.Bookings.Remove(booking);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Booking>> GetAllAsync() => await context.Bookings
        .AsNoTracking()
        .OrderBy(b => b.CreatedAt)
        .ToListAsync();


    public async Task<Booking?> GetByIdAsync(Guid id) => await context.Bookings.FindAsync(id);


    public async Task UpdateAsync(Booking booking) => await context.SaveChangesAsync();

}
