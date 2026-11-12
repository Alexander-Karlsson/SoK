using Bookings.Application.Bookings.DTOs;
using Bookings.Application.Bookings.DTOs.Request;
using Bookings.Application.Bookings.Interfaces;
using Bookings.Application.Bookings.Validation;
using Bookings.Domain.Entities;
using Bookings.Domain.Enums;

namespace Bookings.Application.Bookings.Services;

public class BookingAppService(IBookingRepository repo)
{
    public async Task<BookingDetails> CreateBookingAsync(CreateBookingRequest request)
    {
        BookingValidation.ValidateCreate(request);

        var booking = new Booking
        { 
            CreatedAt = DateTime.UtcNow,
            StartsAt = request.StartsAt,
            EndsAt = request.EndsAt,
            CustomerId = request.CustomerId,
            ProjectId = request.ProjectId
        };

        var employees = request.EmployeeIds
            .Distinct()
            .Select(id => new BookingEmployee {BookingId = booking.Id, EmployeeId = id })
            .ToList();

        var services = request.Services
            .Select(s => new BookingService
            {
                BookingId = booking.Id,
                ServiceId = s.ServiceId,
                EstimatedHours = s.EstimatedHours,
                PricePerHour = s.PricePerHour
            })
            .ToList();

        var machines = request.Machines
            .Select(m => new BookingMachine
            {
                BookingId = booking.Id,
                MachineId = m.MachineId,
                PricePerDay = m.PricePerDay
            })
            .ToList();

        var details = new BookingDetails(booking, employees, services, machines);
        await repo.AddAsync(details);

        return details;
    }

    public async Task<bool> UpdateBookingAsync(Guid id, UpdateBookingRequest request)
    {
        BookingValidation.ValidateUpdate(request);

        var booking = await repo.GetByIdAsync(id);

        if (booking is null)
            return false;

        if (booking.Status is BookingStatus.Cancelled or BookingStatus.Completed)
            throw new InvalidOperationException("En avbokad eller avslutad bokning kan inte ändras.");

        booking.ProjectId = request.ProjectId;
        booking.StartsAt = request.StartsAt;
        booking.EndsAt = request.EndsAt;

        await repo.UpdateAsync(booking);

        return true;
    }
    public async Task<bool> CancelBookingAsync(Guid id)
    {
        var booking = await repo.GetByIdAsync(id);

        if (booking is null)
            return false;

        if (booking.Status == BookingStatus.Completed || booking.InvoiceId is not null)
            throw new InvalidOperationException("En avslutad eller fakturerad bokning kan inte avbokas.");

        booking.Status = BookingStatus.Cancelled;
        await repo.UpdateAsync(booking);

        return true;
    }

    public async Task<bool> DeleteBookingAsync(Guid id)
    {
        var booking = await repo.GetByIdAsync(id);

        if (booking is null)
            return false;

        if (booking.InvoiceId is not null)
            throw new InvalidOperationException("En fakturerad bokning kan inte tas bort.");

        await repo.DeleteAsync(booking);

        return true;
    }

    public async Task<BookingDetails?> GetByIdAsync(Guid id) => await repo.GetBookingDetailsAsync(id);

    public async Task<IReadOnlyList<Booking>> GetAllAsync() => await repo.GetAllAsync();



}
