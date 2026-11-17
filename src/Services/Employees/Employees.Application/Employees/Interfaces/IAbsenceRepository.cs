using Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Application.Employees.Interfaces;

public interface IAbsenceRepository
{
    Task<Absence> AddAsync(Absence absence);
    Task<Absence> GetByIdAsync(Guid id);
    Task<List<Absence>> GetByEmployeeIdAsync(Guid employeeId);
    Task<Absence> UpdateAsync(Absence absence);
    Task<Absence> DeleteAsync(Guid id);
}