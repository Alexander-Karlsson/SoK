using Employees.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Employees.Application.Employees.DTOs.Absence;

public class UpdateAbsenceRequest
{
    [EnumDataType(typeof(AbsenceType), ErrorMessage = "Ogiltig typ av frånvaro.")]
    public AbsenceType Type { get; set; } = AbsenceType.Sick;
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    [MaxLength(500, ErrorMessage = "Anledningen får vara högst 500 tecken.")]
    public string Reason { get; set; } = string.Empty;
}
