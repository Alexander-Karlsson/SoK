using Employees.Application.Employees.DTOs.EmployeePayroll;
using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entities;
using Employees.Infrastructure;

namespace Employees.Application.Employees.Services;

public class EmployeePayrollService(
    IEmployeePayrollRepository repository,
    IEmployeeRepository employeeRepository)
{
    public async Task<(PayrollResult Result, EmployeePayrollResponse? Payroll)> CreatePayrollAsync(
        Guid employeeId, CreateEmployeePayrollRequest request)
    {
        var employee = await employeeRepository.GetByIdAsync(employeeId);
        if (employee is null)
            return (PayrollResult.EmployeeNotFound, null);

        var existing = await repository.GetByEmployeeIdAsync(employeeId);
        if (existing is not null)
            return (PayrollResult.AlreadyExists, null);

        var payroll = new EmployeePayroll
        {
            EmployeeId = employeeId,
            Salary = request.Salary,
            TaxRate = request.TaxRate,
            BankName = request.BankName.Trim(),
            BankAccount = request.BankAccount.Trim()
        };

        var created = await repository.AddAsync(payroll);
        return (PayrollResult.Ok, ToResponse(created));
    }

    public async Task<EmployeePayrollResponse?> GetPayrollAsync(Guid employeeId)
    {
        var payroll = await repository.GetByEmployeeIdAsync(employeeId);
        return payroll is null ? null : ToResponse(payroll);
    }

    public async Task<EmployeePayrollResponse?> UpdatePayrollAsync(
        Guid employeeId, UpdateEmployeePayrollRequest request)
    {
        var payroll = await repository.GetByEmployeeIdAsync(employeeId);
        if (payroll is null) return null;

        payroll.Salary = request.Salary;
        payroll.TaxRate = request.TaxRate;
        payroll.BankName = request.BankName.Trim();
        payroll.BankAccount = request.BankAccount.Trim();

        var updated = await repository.UpdateAsync(payroll);
        return ToResponse(updated);
    }

    public Task<bool> DeletePayrollAsync(Guid employeeId)
        => repository.DeleteByEmployeeIdAsync(employeeId);

    private static EmployeePayrollResponse ToResponse(EmployeePayroll p) => new()
    {
        Id = p.Id,
        EmployeeId = p.EmployeeId,
        Salary = p.Salary,
        TaxRate = p.TaxRate,
        BankName = p.BankName,
        BankAccount = p.BankAccount,
    };
}