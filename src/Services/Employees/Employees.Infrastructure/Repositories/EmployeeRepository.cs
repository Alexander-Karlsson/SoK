using Employees.Domain.Entities;
using Employees.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Employees.Infrastructure.Repositories;

public class EmployeeRepository(EmployeesDbContext context) : IEmployeeRepository
{
    public async Task<Employee> AddAsync(Employee employee)
    {
        await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();
        
        return employee;
    }

    public async Task<Employee> GetByIdAsync(Guid id)
    {
        var employee = await context.Employees.FindAsync(id);
        return employee ?? throw new InvalidOperationException("Employee not found");
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await context.Employees
            .AsNoTracking() // Detta gör att entiteterna inte spåras av DbContext, vilket kan förbättra prestandan vid läsoperationer
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .ToListAsync();
    }

    public async Task<Employee> UpdateAsync(Employee employee)
    {
        context.Employees.Update(employee);
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> DeleteAsync(Guid id)
    {
        var employee = await context.Employees.FindAsync(id);
        if (employee == null) throw new InvalidOperationException("Employee not found");

        context.Employees.Remove(employee);
        await context.SaveChangesAsync();
        return employee;
    }
}