namespace Employees.Application.Employees.DTOs.EmployeePayrol;

public class UpdateEmployeePayrollRequest
{
    public required decimal Salary { get; set; }
    public required int TaxTable { get; set; }
    public decimal Bonus { get; set; }
    public required string BankName { get; set; }
    public required string BankAccount { get; set; }
    public decimal Deductions { get; set; }
    public required DateTime PayrollDate { get; set; }
}
