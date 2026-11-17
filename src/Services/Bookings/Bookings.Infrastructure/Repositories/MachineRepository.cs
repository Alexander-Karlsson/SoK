using Bookings.Application.Bookings.Interfaces;
using Bookings.Domain.Entities;
using Bookings.Domain.Enums;
using Bookings.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Repositories;

public class MachineRepository(BookingDbContext context) : IMachineRepository
{
    public async Task<Machine> AddAsync(Machine machine)
    {
        context.Machines.Add(machine);
        await context.SaveChangesAsync();
        return machine;
    }

    public async Task DeleteAsync(Machine machine)
    {
        context.Machines.Remove(machine);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Machine>> GetAllAsync() => await context.Machines
        .AsNoTracking()
        .OrderBy(m => m.Name)
        .ToListAsync();


    public async Task<Machine?> GetByIdAsync(Guid id) => await context.Machines.FindAsync(id);


    public async Task<IReadOnlyList<Machine>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        var idList = ids.Distinct().ToList();

        return await context.Machines
            .AsNoTracking()
            .Where(m => idList.Contains(m.Id))
            .ToListAsync();
    }

    public async Task<bool> IsInUseAsync(Guid id) => await context.BookingMachines.AnyAsync(bm => bm.MachineId == id);

    public async Task UpdateAsync(Machine machine) => await context.SaveChangesAsync();

    public async Task<bool> RegistrationNumberExistsAsync(string registrationNumber, Guid? excludeId = null)
    => await context.Machines.AnyAsync(m =>
        m.RegistrationNumber == registrationNumber && m.Id != excludeId);

    public async Task<bool> IsAnyBookedAsync(List<Guid> machineIds, DateTime startsAt, DateTime endsAt, Guid? excludeBookingId = null)
    => await context.Bookings.AnyAsync(b =>
        b.Id != excludeBookingId &&
        b.Status != BookingStatus.Cancelled &&
        b.Status != BookingStatus.Completed &&
        b.StartsAt < endsAt &&
        b.EndsAt > startsAt &&
        context.BookingMachines.Any(bm => bm.BookingId == b.Id && machineIds.Contains(bm.MachineId)));
}
