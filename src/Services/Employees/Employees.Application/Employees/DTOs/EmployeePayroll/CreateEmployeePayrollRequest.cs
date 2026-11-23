using System.ComponentModel.DataAnnotations;

namespace Employees.Application.Employees.DTOs.EmployeePayroll;

public class CreateEmployeePayrollRequest
{
    [Range(0.01, 10_000_000, ErrorMessage = "Måste vara mellan 0.01-10.000.000")]
    public decimal Salary { get; set; }
    [Range(0, 100)]
    public decimal TaxRate { get; set; }
    [Required, MaxLength(100)]
    public string BankName { get; set; } = string.Empty;
    [Required, MaxLength(50)]
    public string BankAccount { get; set; } = string.Empty;
}