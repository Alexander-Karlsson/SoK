using Bookings.Application.Bookings.Interfaces;
using Bookings.Domain.Entities;

namespace Bookings.Infrastructure.Repositories;

public class MachineRepository : IMachineRepository
{
    public Task<Machine> AddAsync(Machine machine)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Machine machine)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Machine>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Machine?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Machine>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsInUseAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Machine machine)
    {
        throw new NotImplementedException();
    }
}
