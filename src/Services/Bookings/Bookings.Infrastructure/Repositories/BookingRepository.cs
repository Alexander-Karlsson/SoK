using Bookings.Application.Bookings.DTOs;
using Bookings.Application.Bookings.DTOs.Response;
using Bookings.Application.Bookings.Interfaces;
using Bookings.Domain.Entities;
using Bookings.Domain.Enums;
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

    public async Task<IReadOnlyList<Booking>> GetAllAsync(Guid? customerId = null)
    {
        var query = context.Bookings.AsNoTracking();

        if (customerId is not null)
            query = query.Where(b => b.CustomerId == customerId);

        return await query
            .OrderBy(b => b.CreatedAt)
            .ToListAsync();
    }
        

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

    public async Task<bool> IsAnyEmployeeBookedAsync(List<Guid> employeeIds, DateTime startsAt, DateTime endsAt, Guid? excludeBookingId = null)
    => await context.Bookings.AnyAsync(b =>
        b.Id != excludeBookingId &&
        b.Status != BookingStatus.Cancelled &&
        b.Status != BookingStatus.Completed &&
        b.StartsAt < endsAt &&
        b.EndsAt > startsAt &&
        (context.BookingEmployees.Any(be => be.BookingId == b.Id && employeeIds.Contains(be.EmployeeId)) ||
         context.BookingServices.Any(bs => bs.BookingId == b.Id && bs.EmployeeId != null && employeeIds.Contains(bs.EmployeeId.Value))));

    public async Task<bool> TryAssignServiceAsync(Guid bookingId, Guid serviceId, Guid employeeId)
    => await context.BookingServices
        .Where(bs => bs.BookingId == bookingId && bs.ServiceId == serviceId && bs.EmployeeId == null)
        .ExecuteUpdateAsync(s => s.SetProperty(bs => bs.EmployeeId, (Guid?)employeeId)) == 1;

    public async Task<bool> TryReleaseServiceAsync(Guid bookingId, Guid serviceId, Guid employeeId)
    => await context.BookingServices
        .Where(bs => bs.BookingId == bookingId && bs.ServiceId == serviceId && bs.EmployeeId == employeeId)
        .ExecuteUpdateAsync(s => s.SetProperty(bs => bs.EmployeeId, (Guid?)null)) == 1;

    public async Task<IReadOnlyList<OpenAssignmentResponse>> GetOpenAssignmentsAsync(string? title, DateTime now)
    => await (from bs in context.BookingServices
              join b in context.Bookings on bs.BookingId equals b.Id
              join s in context.BusinessServices on bs.ServiceId equals s.Id
              where bs.EmployeeId == null
                 && b.Status == BookingStatus.Booked
                 && b.EndsAt > now
                 && (s.RequiredTitle == null || s.RequiredTitle == title)
              orderby b.StartsAt
              select new OpenAssignmentResponse(b.Id, bs.ServiceId, bs.Name, bs.EstimatedHours, b.StartsAt, b.EndsAt))
                .AsNoTracking()
                .ToListAsync();

    public async Task<IReadOnlyList<MyAssignmentResponse>> GetAssignmentsForEmployeeAsync(Guid employeeId)
    {
        // Tjänsterader den anställda har tagit
        var services = await (from bs in context.BookingServices
                              join b in context.Bookings on bs.BookingId equals b.Id
                              where bs.EmployeeId == employeeId
                                 && b.Status == BookingStatus.Booked
                              select new MyAssignmentResponse(b.Id, b.StartsAt, b.EndsAt, b.Status, bs.ServiceId, bs.Name, bs.EstimatedHours))
                                .ToListAsync();

        // Bokningar där admin har lagt till den anställda
        var bookings = await (from be in context.BookingEmployees
                              join b in context.Bookings on be.BookingId equals b.Id
                              where be.EmployeeId == employeeId
                                 && b.Status == BookingStatus.Booked
                              select new MyAssignmentResponse(b.Id, b.StartsAt, b.EndsAt, b.Status, null, null, null))
                                .ToListAsync();

        return services.Concat(bookings)
            .OrderBy(a => a.StartsAt)
            .ToList();
    }

    public async Task AddTimeEntryAsync(TimeEntry entry)
    {
        context.TimeEntries.Add(entry);
        await context.SaveChangesAsync();
    }

    public async Task<decimal> GetReportedHoursAsync(Guid employeeId, DateOnly workDate)
        => await context.TimeEntries
            .Where(t => t.EmployeeId == employeeId && t.WorkDate == workDate)
            .SumAsync(t => t.Hours);

    public async Task<IReadOnlyList<TimeEntry>> GetTimeEntriesForBookingAsync(Guid bookingId)
        => await (from t in context.TimeEntries
                  join bs in context.BookingServices on t.BookingServiceId equals bs.Id
                  where bs.BookingId == bookingId
                  orderby t.WorkDate, t.CreatedAt
                  select t)
            .AsNoTracking()
            .ToListAsync();
}
