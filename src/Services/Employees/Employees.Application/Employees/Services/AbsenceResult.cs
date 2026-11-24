namespace Employees.Application.Employees.Services;

public enum AbsenceResult
{
    Ok,
    EmployeeNotFound,
    AbsenceNotFound,
    InvalidDates,
    Overlapping
}