using Employees.Application.Employees.DTOs.Employee;
using Employees.Domain.Entities;
using Employees.Infrastructure;

namespace Employees.Application.Employees.Services;

public class EmployeeService(IEmployeeRepository repository)
{
    public async Task<EmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest request)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            PersonalNumber = request.PersonalNumber.Trim(),
            Email = request.Email.Trim(),
            PhoneNumber = request.PhoneNumber.Trim(),

            EmergencyContactName = request.EmergencyContactName.Trim(),
            EmergencyContactNumber = request.EmergencyContactNumber.Trim(),
            Relationship = request.Relationship.Trim(),

            Title = request.Title.Trim(),
            Department = request.Department,
            EmployeeType = request.EmployeeType,
            EmploymentDate = request.EmploymentStartDate.ToDateTime(TimeOnly.MinValue),
            VacationDaysPerYear = request.VacationDaysPerYear,
            IsActive = true
        };

        var created = await repository.AddAsync(employee);
        return ToResponse(created);
    }

    public async Task<EmployeeResponse> GetEmployeeByIdAsync(Guid id)
    {
        var employee = await repository.GetByIdAsync(id);
        return ToResponse(employee);
    }

    public async Task<List<EmployeeResponse>> GetAllEmployeesAsync()
    {
        var employees = await repository.GetAllAsync();
        return employees.Select(ToResponse).ToList();
    }

    public async Task<EmployeeResponse> UpdateEmployeeAsync(Guid id, UpdateEmployeeRequest request)
    {
        var employee = await repository.GetByIdAsync(id);

        employee.FirstName = request.FirstName.Trim();
        employee.LastName = request.LastName.Trim();
        employee.Email = request.Email.Trim();
        employee.PhoneNumber = request.PhoneNumber.Trim();

        employee.EmergencyContactName = request.EmergencyContactName.Trim();
        employee.EmergencyContactNumber = request.EmergencyContactNumber.Trim();
        employee.Relationship = request.Relationship.Trim();

        employee.Title = request.Title.Trim();
        employee.Department = request.Department;
        employee.EmployeeType = request.EmployeeType;
        employee.EmploymentDate = request.EmploymentStartDate.ToDateTime(TimeOnly.MinValue);
        employee.VacationDaysPerYear = request.VacationDaysPerYear;

        var updated = await repository.UpdateAsync(employee);
        return ToResponse(updated);
    }

    public Task DeleteEmployeeAsync(Guid id)
    {
        return repository.DeleteAsync(id);
    }

    private static EmployeeResponse ToResponse(Employee e) => new()
    {
        Id = e.Id,
        FirstName = e.FirstName,
        LastName = e.LastName,
        Email = e.Email,
        PhoneNumber = e.PhoneNumber,
        Title = e.Title,
        Department = e.Department,
        EmployeeType = e.EmployeeType,
        EmploymentStartDate = DateOnly.FromDateTime(e.EmploymentDate),
        VacationDaysPerYear = e.VacationDaysPerYear
    };
}