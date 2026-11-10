using Employees.Application.Employees.DTOs.Employee;
using Employees.Domain.Entites;
using Employees.Infrastructure;

namespace Employees.Application.Employees.Services;

public class EmployeeService(IEmployeeRepository repository)
{
    public async Task<Employee> CreateEmployeeAsync(CreateEmployeeRequest request)
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };
        await repository.AddAsync(employee);
        return employee;
    }

    public async Task<Employee> GetEmployeeByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<Employee> UpdateEmployeeAsync(Guid id, CreateEmployeeRequest request)
    {
        var employee = await repository.GetByIdAsync(id);
        {
            if (employee == null)
            {
                throw new Exception("Employee not found");
            }
            
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Email = request.Email;
        };
        await repository.UpdateAsync(employee);
        return employee;
    }

    public async Task DeleteEmployeeAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }
}
