using Employees.Domain.Entities;

namespace Employees.Application.Employees.Interfaces;

public interface IPayslipRepository
{
    Task<Payslip?> GetByIdAsync(Guid id);
    Task<List<Payslip>> GetByEmployeeIdAsync(Guid employeeId);
    Task<bool> ExistsAsync(Guid employeeId, int year, int month);
    Task<Payslip> AddAsync(Payslip payslip);
    Task<bool> DeleteAsync(Guid id);
}