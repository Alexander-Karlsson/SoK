namespace Employees.Application.Employees.DTOs.EmployeePayrol;

public class EmployeePayrollResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal Salary { get; set; }
    public int TaxTable { get; set; }
    public decimal Bonus { get; set; }
    public string BankName { get; set; } = string.Empty;
    public string BankAccount { get; set; } = string.Empty;
    public decimal Deductions { get; set; }
    public DateTime PayrollDate { get; set; }
}
