using Employees.Domain.Entities;

namespace Employees.Application.Employees.Interfaces;

public interface IEmployeePayrollRepository
{
    Task<EmployeePayroll?> GetByEmployeeIdAsync(Guid employeeId);
    Task<EmployeePayroll> AddAsync(EmployeePayroll employeePayroll);
    Task<EmployeePayroll> UpdateAsync(EmployeePayroll employeePayroll);
    Task<bool> DeleteByEmployeeIdAsync(Guid employeeId);
}
