using System.ComponentModel.DataAnnotations;

namespace Employees.Application.Employees.DTOs.EmployeePayroll;

public class UpdateEmployeePayrollRequest
{
    [Range(0.01, 10_000_000, ErrorMessage = "Måste vara mellan 0.01-10.000.000")]
    public decimal Salary { get; set; }
    [Range(0, 100, ErrorMessage = "Måste vara mellan 0-100")]
    public decimal TaxRate { get; set; }
    [Required(ErrorMessage = "Måste ange bank")]
    [MaxLength(100, ErrorMessage = "Kan inte vara längre än 100 tecken")]
    public string BankName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Måste ange banknummer")]
    [MaxLength(50, ErrorMessage = "Kan inte vara längre än 50 tecken")]
    public string BankAccount { get; set; } = string.Empty;
}
