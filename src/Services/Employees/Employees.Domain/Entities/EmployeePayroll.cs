namespace Employees.Domain.Entities;

public class EmployeePayroll
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EmployeeId { get; set; }
    public decimal Salary { get; set; }
    public decimal TaxRate { get; set; }
    public string BankName { get; set; } = string.Empty;
    public string BankAccount { get; set; } = string.Empty;

}