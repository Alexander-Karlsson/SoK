using System.ComponentModel.DataAnnotations;

namespace Employees.Application.Employees.DTOs.Payslip;

public class CreatePayslipRequest
{
    [Range(1980, 2100, ErrorMessage = "Ange ett giltigt år.")]
    public int Year { get; set; } = DateTime.Today.Year;

    [Range(1, 12, ErrorMessage = "Månad måste vara mellan 1 och 12.")]
    public int Month { get; set; } = DateTime.Today.Month;

    [Range(0, 1_000_000, ErrorMessage = "Bonus kan inte vara negativ.")]
    public decimal Bonus { get; set; }

    [Range(0, 1_000_000, ErrorMessage = "Avdrag kan inte vara negativa.")]
    public decimal OtherDeductions { get; set; }
}