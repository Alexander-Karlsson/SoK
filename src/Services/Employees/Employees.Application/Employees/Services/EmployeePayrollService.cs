using Employees.Application.Employees.DTOs.EmployeePayrol;
using Employees.Application.Employees.DTOs.EmployeePayroll;
using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entities;

namespace Employees.Application.Employees.Services;

public class EmployeePayrollService(IEmployeePayrollRepository repository)
{
    public async Task<EmployeePayrollResponse> CreatePayrollAsync(CreateEmployeePayrollRequest request)
    {
        var payroll = new EmployeePayroll
        {
            EmployeeId = request.EmployeeId,
            Salary = request.Salary,
            TaxTable = request.TaxTable,
            Bonus = request.Bonus,
            BankName = request.BankName.Trim(),
            BankAccount = request.BankAccount.Trim(),
            Deductions = request.Deductions,
            PayrollDate = request.PayrollDate
        };

        var created = await repository.AddAsync(payroll);
        return ToResponse(created);
    }

    public async Task<EmployeePayrollResponse> GetPayrollByIdAsync(Guid id)
    {
        var payroll = await repository.GetByIdAsync(id);
        return ToResponse(payroll);
    }

    public async Task<List<EmployeePayrollResponse>> GetPayrollsByEmployeeIdAsync(Guid employeeId)
    {
        var payrolls = await repository.GetByEmployeeIdAsync(employeeId);
        return payrolls.Select(ToResponse).ToList();
    }

    public async Task<EmployeePayrollResponse> UpdatePayrollAsync(Guid id, UpdateEmployeePayrollRequest request)
    {
        var payroll = await repository.GetByIdAsync(id);

        payroll.Salary = request.Salary;
        payroll.TaxTable = request.TaxTable;
        payroll.Bonus = request.Bonus;
        payroll.BankName = request.BankName.Trim();
        payroll.BankAccount = request.BankAccount.Trim();
        payroll.Deductions = request.Deductions;
        payroll.PayrollDate = request.PayrollDate;

        var updated = await repository.UpdateAsync(payroll);
        return ToResponse(updated);
    }

    public Task DeletePayrollAsync(Guid id)
    {
        return repository.DeleteAsync(id);
    }

    private static EmployeePayrollResponse ToResponse(EmployeePayroll p) => new()
    {
        Id = p.Id,
        EmployeeId = p.EmployeeId,
        Salary = p.Salary,
        TaxTable = p.TaxTable,
        Bonus = p.Bonus,
        BankName = p.BankName,
        BankAccount = p.BankAccount,
        Deductions = p.Deductions,
        PayrollDate = p.PayrollDate
    };
}