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

    public async Task<bool> DeleteAsync(Guid id)
    {
        var absence = await Context.Absences.FindAsync(id);
        if (absence is null) return false;

        Context.Absences.Remove(absence);
        await Context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> HasOverlapAsync(Guid employeeId, DateOnly start, DateOnly end, Guid? exceptId = null)
    {
        return await Context.Absences.AnyAsync(a =>
            a.EmployeeId == employeeId &&
            a.Id != exceptId &&
            a.StartDate <= end &&
            a.EndDate >= start);
    }

    public async Task<Absence?> GetByIdAsync(Guid id)
    {
        return await Context.Absences.FindAsync(id);
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
