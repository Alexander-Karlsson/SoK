using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entities;
using Employees.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Employees.Infrastructure.Repositories;

public class EmployeePayrollRepository(EmployeesDbContext context) : IEmployeePayrollRepository
{
    public async Task<EmployeePayroll> AddAsync(EmployeePayroll employeePayroll)
    {
        await context.EmployeePayrolls.AddAsync(employeePayroll);
        await context.SaveChangesAsync();
        return employeePayroll;
    }

    public async  Task<bool> DeleteByEmployeeIdAsync(Guid employeeId)
    {
        var payroll = await context.EmployeePayrolls
        .FirstOrDefaultAsync(p => p.EmployeeId == employeeId);

        if (payroll == null)
        {
            return false;
        }

        context.EmployeePayrolls.Remove(payroll);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<EmployeePayroll?> GetByEmployeeIdAsync(Guid employeeId)
    {
        return await context.EmployeePayrolls
            .FirstOrDefaultAsync(p => p.EmployeeId == employeeId);
    }

    public async Task<EmployeePayroll> UpdateAsync(EmployeePayroll employeePayroll)
    {
        context.EmployeePayrolls.Update(employeePayroll);
        await context.SaveChangesAsync();
        return employeePayroll;
    }
}
