namespace Employees.Application.Employees.DTOs.EmployeePayroll;

public class EmployeePayrollResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal Salary { get; set; }
    public decimal TaxRate { get; set; }
    public string BankName { get; set; } = string.Empty;
    public string BankAccount { get; set; } = string.Empty;
}
