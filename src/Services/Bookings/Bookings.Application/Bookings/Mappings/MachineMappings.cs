using Bookings.Application.Bookings.DTOs.Response;
using Bookings.Domain.Entities;

namespace Bookings.Application.Bookings.Mappings;

public static class MachineMappings
{
    public static MachineResponse ToResponse(this Machine machine) => new(
        machine.Id,
        machine.Name,
        machine.Type,
        machine.RegistrationNumber,
        machine.PricePerDay,
        machine.IsOutOfService
        );
}
