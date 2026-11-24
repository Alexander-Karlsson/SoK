using Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Application.Employees.Interfaces;

public interface IAbsenceRepository
{
    Task<Absence?> GetByIdAsync(Guid id);
    Task<List<Absence>> GetByEmployeeIdAsync(Guid employeeId);
    Task<bool> HasOverlapAsync(Guid employeeId, DateOnly start, DateOnly end, Guid? exceptId = null);
    Task<Absence> AddAsync(Absence absence);
    Task<Absence> UpdateAsync(Absence absence);
    Task<bool> DeleteAsync(Guid id);
}