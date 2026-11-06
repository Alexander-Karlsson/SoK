using Bookings.Application.Bookings.DTOs;

namespace Bookings.Application.Bookings.Validation;

public static class BookingValidation
{
    public static void ValidateCreate(CreateBookingRequest request)
    {
        if (request.CustomerId == Guid.Empty)
            throw new ArgumentException("CustomerId saknas.");

        if (request.ProjectId == Guid.Empty)
            throw new ArgumentException("ProjectId är ogiltigt.");

        if (request.EmployeeIds.Count == 0 && request.Services.Count == 0 && request.Machines.Count == 0)
            throw new ArgumentException("Bokningen måste innehålla minst en anställd, tjänst eller maskin.");

        ValidateDates(request.StartsAt, request.EndsAt);
        ValidateEmployees(request.EmployeeIds);
        ValidateServices(request.Services);
        ValidateMachines(request.Machines);
    }

    public static void ValidateUpdate(UpdateBookingRequest request)
    {
        if (request.ProjectId == Guid.Empty)
            throw new ArgumentException("ProjectId är ogiltigt.");

        ValidateDates(request.StartsAt, request.EndsAt);
    }

    public static void ValidateDates(DateTime startsAt, DateTime? endsAt)
    {
        if (endsAt is not null && endsAt <= startsAt)
            throw new ArgumentException("EndsAt måste vara efter StartsAt.");
    }

    private static void ValidateEmployees(List<Guid> employeeIds)
    {
        if (employeeIds.Contains(Guid.Empty))
            throw new ArgumentException("EmployeeIds innehåller ett ogiltigt id.");
    }

    private static void ValidateServices(List<CreateBookingServiceRequest> services)
    {
        if (services.Any(s => s.ServiceId == Guid.Empty || s.EstimatedHours < 0 || s.PricePerHour < 0))
            throw new ArgumentException("Services innehåller ogiltiga värden.");

        if (services.DistinctBy(s => s.ServiceId).Count() != services.Count)
            throw new ArgumentException("Samma tjänst finns flera gånger.");
    }

    private static void ValidateMachines(List<CreateBookingMachineRequest> machines)
    {
        if (machines.Any(m => m.MachineId == Guid.Empty || m.PricePerDay < 0))
            throw new ArgumentException("Machines innehåller ogiltiga värden.");

        if (machines.DistinctBy(m => m.MachineId).Count() != machines.Count)
            throw new ArgumentException("Samma maskin finns flera gånger.");
    }
}

