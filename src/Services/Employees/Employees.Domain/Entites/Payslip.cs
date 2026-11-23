namespace Employees.Domain.Entites;

public class Payslip
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EmployeeId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal Bonus { get; set; }
    public decimal AbsenceDeduction { get; set; }
    public decimal OtherDeductions { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal NetSalary { get; set; }
    public DateOnly PaymentDate { get; set; }

}
