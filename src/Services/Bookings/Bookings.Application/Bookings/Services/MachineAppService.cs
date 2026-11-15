using Bookings.Application.Bookings.DTOs.Request;
using Bookings.Application.Bookings.Interfaces;
using Bookings.Application.Bookings.Validation;
using Bookings.Domain.Entities;

namespace Bookings.Application.Bookings.Services;

public class MachineAppService(IMachineRepository repo)
{
    public async Task<Machine> CreateMachineAsync(CreateMachineRequest request)
    {
        MachineValidation.Validate(request.Name, request.Type, request.RegistrationNumber, request.PricePerDay);

        var registrationNumber = Normalize(request.RegistrationNumber);

        if (await repo.RegistrationNumberExistsAsync(registrationNumber))
            throw new InvalidOperationException("Registreringsnumret finns redan.");

        var machine = new Machine
        {
            Name = request.Name.Trim(),
            Type = request.Type.Trim(),
            RegistrationNumber = registrationNumber,
            PricePerDay = request.PricePerDay
        };

        return await repo.AddAsync(machine);
    }

    public async Task<Machine?> GetByIdAsync(Guid id)
        => await repo.GetByIdAsync(id);

    public async Task<IReadOnlyList<Machine>> GetAllAsync()
        => await repo.GetAllAsync();

    public async Task<bool> UpdateMachineAsync(Guid id, UpdateMachineRequest request)
    {
        MachineValidation.Validate(request.Name, request.Type, request.RegistrationNumber, request.PricePerDay);

        var machine = await repo.GetByIdAsync(id);

        if (machine is null)
            return false;

        var registrationNumber = Normalize(request.RegistrationNumber);

        if (await repo.RegistrationNumberExistsAsync(registrationNumber, id))
            throw new InvalidOperationException("Registreringsnumret finns redan.");

        machine.Name = request.Name.Trim();
        machine.Type = request.Type.Trim();
        machine.RegistrationNumber = registrationNumber;
        machine.PricePerDay = request.PricePerDay;
        machine.IsOutOfService = request.IsOutOfService;

        await repo.UpdateAsync(machine);

        return true;
    }

    public async Task<bool> DeleteMachineAsync(Guid id)
    {
        var machine = await repo.GetByIdAsync(id);

        if (machine is null)
            return false;

        if (await repo.IsInUseAsync(id))
            throw new InvalidOperationException("Maskinen finns i bokningar och kan inte tas bort. Markera den som ur drift i stället.");

        await repo.DeleteAsync(machine);

        return true;
    }

    private static string Normalize(string registrationNumber)
        => registrationNumber.Trim().ToUpperInvariant();
}