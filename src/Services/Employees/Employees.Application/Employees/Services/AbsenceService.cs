using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entites;

namespace Employees.Application.Employees.Services;

public class AbsenceService(IAbsenceRepository repository)
{
    public async Task<Absence> AddAsync(Absence absence)
    {
        return await repository.AddAsync(absence);
    }

    public async Task<Absence> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<Absence> UpdateAsync(Absence absence)
    {
        return await repository.UpdateAsync(absence);
    }

    public async Task<Absence> DeleteAsync(Guid id)
    {
        return await repository.DeleteAsync(id);
    }
}
