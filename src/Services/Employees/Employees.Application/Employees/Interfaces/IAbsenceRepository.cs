using Employees.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Application.Employees.Interfaces;

public interface IAbsenceRepository
{
    Task<Absence> AddAsync(Absence absence);
    Task<Absence> GetByIdAsync(Guid id);
    Task<Absence> UpdateAsync(Absence absence);
    Task<Absence> DeleteAsync(Guid id);
}