using Bookings.Application.Bookings.DTOs;
using Bookings.Application.Bookings.DTOs.Request;
using Bookings.Application.Bookings.Interfaces;
using Bookings.Application.Bookings.Validation;
using Bookings.Domain.Entities;
using Bookings.Domain.Enums;

namespace Bookings.Application.Bookings.Services;

public class BookingAppService(IBookingRepository repo, IMachineRepository machineRepo, IBusinessServiceRepository serviceRepo)
{
    public async Task<BookingDetails> CreateBookingAsync(CreateBookingRequest request)
    {
        BookingValidation.ValidateCreate(request);

        var endsAt = request.EndsAt ?? request.StartsAt.AddDays(BookingValidation.MaxRentalDays);

        var booking = new Booking
        {
            CreatedAt = DateTime.UtcNow,
            StartsAt = request.StartsAt,
            EndsAt = endsAt,
            CustomerId = request.CustomerId,
            ProjectId = request.ProjectId
        };

        var employees = request.EmployeeIds
            .Distinct()
            .Select(id => new BookingEmployee { BookingId = booking.Id, EmployeeId = id })
            .ToList();

        var services = await BuildServicesAsync(booking.Id, request.Services);

        var machines = await BuildMachinesAsync(booking.Id, request.Machines);

        if (request.Machines.Count > 0 &&
    await machineRepo.IsAnyBookedAsync(request.Machines.Select(m => m.MachineId).ToList(), request.StartsAt, endsAt))
            throw new InvalidOperationException("En eller flera maskiner är redan bokade under perioden.");

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

        var endsAt = request.EndsAt ?? request.StartsAt.AddDays(BookingValidation.MaxRentalDays);

        var details = await repo.GetBookingDetailsAsync(id);

        if (details!.Machines.Count > 0 &&
            await machineRepo.IsAnyBookedAsync(details.Machines.Select(m => m.MachineId).ToList(), request.StartsAt, endsAt, id))
            throw new InvalidOperationException("En eller flera maskiner är redan bokade under perioden.");

        booking.ProjectId = request.ProjectId;
        booking.StartsAt = request.StartsAt;
        booking.EndsAt = endsAt;

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

    private async Task<List<BookingMachine>> BuildMachinesAsync(Guid bookingId, List<CreateBookingMachineRequest> requested)
    {
        if (requested.Count == 0)
            return [];

        var catalog = (await machineRepo.GetByIdsAsync(requested.Select(m => m.MachineId)))
            .ToDictionary(m => m.Id);

        var result = new List<BookingMachine>();

        foreach (var m in requested)
        {
            if (!catalog.TryGetValue(m.MachineId, out var machine))
                throw new ArgumentException($"Maskinen {m.MachineId} finns inte.");

            if (machine.IsOutOfService)
                throw new InvalidOperationException($"Maskinen {machine.Name} är ur drift.");

            result.Add(new BookingMachine
            {
                BookingId = bookingId,
                MachineId = machine.Id,
                PricePerDay = machine.PricePerDay
            });
        }

        return result;
    }

    private async Task<List<BookingService>> BuildServicesAsync(Guid bookingId, List<CreateBookingServiceRequest> requested)
    {
        if (requested.Count == 0)
            return [];

        var catalog = (await serviceRepo.GetByIdsAsync(requested.Select(s => s.ServiceId)))
            .ToDictionary(s => s.Id);

        var result = new List<BookingService>();

        foreach (var s in requested)
        {
            if (!catalog.TryGetValue(s.ServiceId, out var service))
                throw new ArgumentException($"Tjänsten {s.ServiceId} finns inte.");

            if (!service.IsActive)
                throw new InvalidOperationException($"Tjänsten {service.Name} är inte aktiv.");

            result.Add(new BookingService
            {
                BookingId = bookingId,
                ServiceId = service.Id,
                EstimatedHours = s.EstimatedHours,
                PricePerHour = service.PricePerHour
            });
        }

        return result;
    }

}
