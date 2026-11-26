using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entities;
using Employees.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Employees.Infrastructure.Repositories;

public class PayslipRepository(EmployeesDbContext context) : IPayslipRepository
{
    public async Task<Payslip?> GetByIdAsync(Guid id)
    {
        return await context.Payslips
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Payslip>> GetByEmployeeIdAsync(Guid employeeId)
    {
        return await context.Payslips
            .Where(p => p.EmployeeId == employeeId)
            .OrderByDescending(p => p.Year)
            .ThenByDescending(p => p.Month)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(Guid employeeId, int year, int month)
    {
        return await context.Payslips
            .AnyAsync(p => p.EmployeeId == employeeId && p.Year == year && p.Month == month);
    }
    
    public async Task<Payslip> AddAsync(Payslip payslip)
    {
        await context.Payslips.AddAsync(payslip);
        await context.SaveChangesAsync();
        return payslip;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var payslip = await context.Payslips
        .FirstOrDefaultAsync(p => p.Id == id);

        if (payslip == null)
        {
            return false;
        }

        context.Payslips.Remove(payslip);
        await context.SaveChangesAsync();
        return true;
    }
}
