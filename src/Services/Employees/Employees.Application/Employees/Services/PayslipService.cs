using Employees.Application.Employees.DTOs.Payslip;
using Employees.Application.Employees.Interfaces;
using Employees.Domain.Entities;
using Employees.Domain.Enums;
using Employees.Infrastructure;

namespace Employees.Application.Employees.Services;

public class PayslipService(
    IPayslipRepository repository,
    IEmployeeRepository employeeRepository,
    IEmployeePayrollRepository payrollRepository,
    IAbsenceRepository absenceRepository)
{
    // Förenklat: en månad räknas alltid som 21 arbetsdagar
    private const decimal WorkDaysPerMonth = 21m;

    public async Task<(PayslipResult Result, PayslipResponse? Payslip)> CreatePayslipAsync(
        Guid employeeId, CreatePayslipRequest request)
    {
        var employee = await employeeRepository.GetByIdAsync(employeeId);
        if (employee is null)
            return (PayslipResult.EmployeeNotFound, null);

        var payroll = await payrollRepository.GetByEmployeeIdAsync(employeeId);
        if (payroll is null)
            return (PayslipResult.PayrollNotFound, null);

        if (await repository.ExistsAsync(employeeId, request.Year, request.Month))
            return (PayslipResult.AlreadyExists, null);

        var absences = await absenceRepository.GetByEmployeeIdAsync(employeeId);
        var absenceDeduction = CalculateAbsenceDeduction(payroll.Salary, absences, request.Year, request.Month);

        var gross = payroll.Salary + request.Bonus - absenceDeduction - request.OtherDeductions;
        if (gross < 0) gross = 0;

        // TaxRate sparas i procent, t.ex. 30 = 30 %
        var tax = Math.Round(gross * payroll.TaxRate / 100, 2);

        var payslip = new Payslip
        {
            EmployeeId = employeeId,
            Year = request.Year,
            Month = request.Month,
            BaseSalary = payroll.Salary,
            TaxRate = payroll.TaxRate,
            Bonus = request.Bonus,
            AbsenceDeduction = absenceDeduction,
            OtherDeductions = request.OtherDeductions,
            GrossSalary = gross,
            TaxAmount = tax,
            NetSalary = gross - tax,
            PaymentDate = new DateOnly(request.Year, request.Month, 25) // förenklat: alltid den 25:e
        };

        var created = await repository.AddAsync(payslip);
        return (PayslipResult.Ok, ToResponse(created));
    }

    public async Task<List<PayslipResponse>> GetPayslipsAsync(Guid employeeId)
    {
        var payslips = await repository.GetByEmployeeIdAsync(employeeId);
        return payslips.Select(ToResponse).ToList();
    }

    public async Task<PayslipResponse?> GetPayslipAsync(Guid employeeId, Guid payslipId)
    {
        var payslip = await repository.GetByIdAsync(payslipId);
        if (payslip is null || payslip.EmployeeId != employeeId) return null;
        return ToResponse(payslip);
    }

    public async Task<bool> DeletePayslipAsync(Guid employeeId, Guid payslipId)
    {
        var payslip = await repository.GetByIdAsync(payslipId);
        if (payslip is null || payslip.EmployeeId != employeeId) return false;
        return await repository.DeleteAsync(payslipId);
    }

    // Räknar ut avdraget för all frånvaro som ligger i den valda månaden
    private static decimal CalculateAbsenceDeduction(
        decimal salary, List<Absence> absences, int year, int month)
    {
        var daySalary = salary / WorkDaysPerMonth;
        var firstDay = new DateOnly(year, month, 1);
        var lastDay = firstDay.AddMonths(1).AddDays(-1);
        decimal deduction = 0;

        foreach (var absence in absences)
        {
            // Vilken frånvarodag i ordningen det är (bara vardagar räknas)
            var dayNumber = 0;

            for (var date = absence.StartDate; date <= absence.EndDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                dayNumber++;

                // Dagen räknas, men dras bara om den ligger i den här månaden
                if (date < firstDay || date > lastDay)
                    continue;

                deduction += daySalary * GetDeductionPercent(absence.AbsenceType, dayNumber);
            }
        }

        return Math.Round(deduction, 2);
    }

    // Hur stor del av dagslönen som dras för en viss dag
    private static decimal GetDeductionPercent(AbsenceType type, int dayNumber)
    {
        switch (type)
        {
            case AbsenceType.Sick:
                if (dayNumber == 1) return 1.00m;    // karensdag
                if (dayNumber <= 14) return 0.20m;   // sjuklön 80 %
                return 1.00m;                        // dag 15+ betalar Försäkringskassan
            case AbsenceType.Vacation:
                return 0m;                           // semester ger inget avdrag
            default:
                return 1.00m;                        // tjänstledigt, VAB, övrigt
        }
    }

    private static PayslipResponse ToResponse(Payslip p) => new()
    {
        Id = p.Id,
        EmployeeId = p.EmployeeId,
        Year = p.Year,
        Month = p.Month,
        BaseSalary = p.BaseSalary,
        TaxRate = p.TaxRate,
        Bonus = p.Bonus,
        AbsenceDeduction = p.AbsenceDeduction,
        OtherDeductions = p.OtherDeductions,
        GrossSalary = p.GrossSalary,
        TaxAmount = p.TaxAmount,
        NetSalary = p.NetSalary,
        PaymentDate = p.PaymentDate
    };
}