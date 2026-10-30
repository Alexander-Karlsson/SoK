using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Domain.Entites;

public class EmployeePayroll
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EmployeeId { get; set; }
    public decimal Salary { get; set; }
    public int TaxTable { get; set; }
    public decimal Bonus { get; set; }
    public string BankName { get; set; } = string.Empty;
    public string BankAccount { get; set; } = string.Empty;
    public decimal Deductions { get; set; }
    public DateTime PayrollDate { get; set; }

}
