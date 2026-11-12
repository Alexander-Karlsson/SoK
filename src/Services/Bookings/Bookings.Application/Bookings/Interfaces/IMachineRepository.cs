using Bookings.Domain.Entities;

namespace Bookings.Application.Bookings.Interfaces;

public interface IMachineRepository
{
    Task<Machine?> GetByIdAsync(Guid id);
    //IEnumerable to get all the machines when booking calls for it, if multiple machines per booking
    Task<IReadOnlyList<Machine>> GetByIdsAsync(IEnumerable<Guid> ids);
    Task<Machine> AddAsync(Machine machine);
    Task DeleteAsync(Machine machine);
    Task UpdateAsync(Machine machine);
    Task<IReadOnlyList<Machine>> GetAllAsync();
    Task<bool> IsInUseAsync(Guid id);
}
