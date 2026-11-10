using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entites;
using Employees.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Infrastructure.Repositories;

public class EmployeePayrollRepository(EmployeesDbContext context) : IEmployeePayrollRepository
{
    public async Task<EmployeePayroll> AddAsync(EmployeePayroll employeePayroll)
    {
        await context.EmployeePayrolls.AddAsync(employeePayroll);
        await context.SaveChangesAsync();
        return employeePayroll;
    }

    public async Task<EmployeePayroll> DeleteAsync(Guid id)
    {
        var employeePayroll = await context.EmployeePayrolls.FindAsync(id);
        if (employeePayroll == null) throw new InvalidOperationException("Employee payroll not found");

        context.EmployeePayrolls.Remove(employeePayroll);
        await context.SaveChangesAsync();
        return employeePayroll;
    }

    public async Task<EmployeePayroll> GetByIdAsync(Guid id)
    {
        var employeePayroll = await context.EmployeePayrolls.FindAsync(id);
        return employeePayroll ?? throw new InvalidOperationException("Employee payroll not found");
    }

    public async Task<EmployeePayroll> UpdateAsync(EmployeePayroll employeePayroll)
    {
        context.EmployeePayrolls.Update(employeePayroll);
        await context.SaveChangesAsync();
        return employeePayroll;
    }
}
