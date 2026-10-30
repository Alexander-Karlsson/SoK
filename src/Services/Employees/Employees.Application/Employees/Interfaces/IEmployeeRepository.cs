using Employees.Domain.Entites;

namespace Employees.Infrastructure;

public interface IEmployeeRepository 
{
    Task<Employee> AddAsync(Employee employee);
    Task<Employee> GetByIdAsync(Guid id);
    Task<Employee> UpdateAsync(Employee employee);
    Task<Employee> DeleteAsync(Guid id);
}