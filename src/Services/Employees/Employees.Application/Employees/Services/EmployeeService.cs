using Employees.Application.Employees.DTOs.Employee;
using Employees.Domain.Entities;
using Employees.Infrastructure;
using System.Reflection.Emit;

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
            Address = request.Address.Trim(),
            Zipcode = request.Zipcode.Trim(),
            City = request.City.Trim(),

            EmergencyContactName = request.EmergencyContactName.Trim(),
            EmergencyContactNumber = request.EmergencyContactNumber.Trim(),
            Relationship = request.Relationship.Trim(),

            Title = request.Title.Trim(),
            Department = request.Department,
            EmployeeType = request.EmployeeType,
            EmploymentDate = request.EmploymentStartDate.ToDateTime(TimeOnly.MinValue),
            EmploymentEndDate = request.EmploymentEndDate?.ToDateTime(TimeOnly.MinValue),
            EmployeeRate = request.EmployeeRate,
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
        employee.PersonalNumber = request.PersonalNumber.Trim();
        employee.Email = request.Email.Trim();
        employee.PhoneNumber = request.PhoneNumber.Trim();
        employee.Address = request.Address.Trim();
        employee.Zipcode = request.Zipcode.Trim();
        employee.City = request.City.Trim();

        employee.EmergencyContactName = request.EmergencyContactName.Trim();
        employee.EmergencyContactNumber = request.EmergencyContactNumber.Trim();
        employee.Relationship = request.Relationship.Trim();

        employee.Title = request.Title.Trim();
        employee.Department = request.Department;
        employee.EmployeeType = request.EmployeeType;
        employee.EmploymentDate = request.EmploymentStartDate.ToDateTime(TimeOnly.MinValue);
        employee.EmploymentEndDate = request.EmploymentEndDate?.ToDateTime(TimeOnly.MinValue);
        employee.EmployeeRate = request.EmployeeRate;
        employee.VacationDaysPerYear = request.VacationDaysPerYear;

        var updated = await repository.UpdateAsync(employee);
        return ToResponse(updated);
    }

    public async Task<EmployeeResponse?> DeactiveEmployeeAsync(Guid id, DeactivateEmployeeRequest request)
    {
        var employee = await repository.GetByIdAsync(id);
        if (employee == null)
        {
            return null;
        }
        employee.IsActive = false;
        employee.EmploymentEndDate = request.EmploymentEndDate.ToDateTime(TimeOnly.MinValue);

        var update = await repository.UpdateAsync(employee);
        return ToResponse(update);
    }
    public async Task<EmployeeResponse?> ReactivateEmployeeAsync(Guid id, ReactivateEmployeeRequest request)
    {
        var employee = await repository.GetByIdAsync(id);

        if (employee == null)
        {
            return null;
        }

        employee.IsActive = true;
        employee.EmploymentDate = request.EmploymentStartDate.ToDateTime(TimeOnly.MinValue);
        employee.EmploymentEndDate = default;

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
        PersonalNumber = e.PersonalNumber,
        Email = e.Email,
        PhoneNumber = e.PhoneNumber,
        Address = e.Address,
        Zipcode = e.Zipcode,
        City = e.City,
        EmergencyContactName = e.EmergencyContactName,
        EmergencyContactNumber = e.EmergencyContactNumber,
        Relationship = e.Relationship,
        EmployeeNumber = e.EmployeeNumber,
        Title = e.Title,
        Department = e.Department,
        EmployeeType = e.EmployeeType,
        EmployeeRate = e.EmployeeRate,
        EmploymentStartDate = DateOnly.FromDateTime(e.EmploymentDate),
        EmploymentEndDate = e.EmploymentEndDate is { } end ? DateOnly.FromDateTime(end) : null,
        VacationDaysPerYear = e.VacationDaysPerYear,
        IsActive = e.IsActive
    };
}