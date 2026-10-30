using Employees.Domain.Entites;
using Employees.Infrastructure.Persistence;

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