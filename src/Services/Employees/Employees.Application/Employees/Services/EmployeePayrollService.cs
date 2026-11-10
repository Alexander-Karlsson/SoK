using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entites;

namespace Employees.Application.Employees.Services;

public class EmployeePayrollService(IEmployeePayrollRepository repository)
{
    public async Task<EmployeePayroll> AddAsync(EmployeePayroll Payroll)
    {
        return await repository.AddAsync(Payroll);
    }

    public async Task<EmployeePayroll> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<EmployeePayroll> UpdateAsync(EmployeePayroll Payroll)
    {
        return await repository.UpdateAsync(Payroll);
    }

    public async Task<EmployeePayroll> DeleteAsync(Guid id)
    {
        return await repository.DeleteAsync(id);
    }
}
