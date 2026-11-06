using Bookings.Application.Bookings.DTOs;
using Bookings.Application.Bookings.Interfaces;
using Bookings.Domain.Entities;
using Bookings.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Repositories;

public class BookingRepository(BookingDbContext context) : IBookingRepository
{
    public async Task<Booking> AddAsync(BookingDetails details)
    {
        context.Bookings.Add(details.Booking);
        context.BookingEmployees.AddRange(details.Employees);
        context.BookingServices.AddRange(details.Services);
        context.BookingMachines.AddRange(details.Machines);
        
        await context.SaveChangesAsync();
        return details.Booking;
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

    public async Task<BookingDetails?> GetBookingDetailsAsync(Guid id)
    {
        var booking = await context.Bookings
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking is null) return null;

        var employees = await context.BookingEmployees
            .AsNoTracking()
            .Where(be => be.BookingId == id)
            .ToListAsync();

        var services = await context.BookingServices
            .AsNoTracking()
            .Where(bs => bs.BookingId == id)
            .ToListAsync();

        var machines = await context.BookingMachines
            .AsNoTracking()
            .Where(bm => bm.BookingId == id)
            .ToListAsync();

        return new BookingDetails(booking, employees, services, machines);
    }

    public async Task<Booking?> GetByIdAsync(Guid id) => await context.Bookings.FindAsync(id);

    public async Task UpdateAsync(Booking booking) => await context.SaveChangesAsync();

}
