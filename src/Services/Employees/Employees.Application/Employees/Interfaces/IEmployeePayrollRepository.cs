using Employees.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Application.Employees.Interfaces;

public interface IEmployeePayrollRepository
{
    Task<EmployeePayroll> AddAsync(EmployeePayroll employeePayroll);
    Task<EmployeePayroll> GetByIdAsync(Guid id);
    Task<EmployeePayroll> UpdateAsync(EmployeePayroll employeePayroll);
    Task<EmployeePayroll> DeleteAsync(Guid id);
}
