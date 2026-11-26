using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entities;
using Employees.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Employees.Infrastructure.Repositories;

public class AbsenceRepository(EmployeesDbContext Context) : IAbsenceRepository
{
    public async Task<Absence> AddAsync(Absence absence)
    {
        await Context.Absences.AddAsync(absence);
        await Context.SaveChangesAsync();
        return absence;
    }

    public async Task<Absence> DeleteAsync(Guid id)
    {
        var absence = await Context.Absences.FindAsync(id);
        if (absence == null) throw new InvalidOperationException("Absence not found");
        
        Context.Absences.Remove(absence);
        await Context.SaveChangesAsync();
        return absence;
    }

    public async Task<Absence> GetByIdAsync(Guid id)
    {
        var absence = await Context.Absences.FindAsync(id);
        return absence ?? throw new InvalidOperationException("Absence not found");
    }

    public async Task<List<Absence>> GetByEmployeeIdAsync(Guid employeeId)
    {
        return await Context.Absences
            .AsNoTracking()
            .Where(a => a.EmployeeId == employeeId)
            .OrderByDescending(a => a.StartDate)
            .ToListAsync();
    }

    public async Task<Absence> UpdateAsync(Absence absence)
    {
        Context.Absences.Update(absence);
        await Context.SaveChangesAsync();
        return absence;
    }
}
