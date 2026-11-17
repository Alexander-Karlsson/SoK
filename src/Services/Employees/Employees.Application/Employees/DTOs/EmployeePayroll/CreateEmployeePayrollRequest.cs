namespace Employees.Application.Employees.DTOs.EmployeePayroll;

public class CreateEmployeePayrollRequest
{
    public required Guid EmployeeId { get; set; }
    public required decimal Salary { get; set; }
    public required int TaxTable { get; set; }
    public decimal Bonus { get; set; }
    public required string BankName { get; set; }
    public required string BankAccount { get; set; }
    public decimal Deductions { get; set; }
    public required DateTime PayrollDate { get; set; }
}