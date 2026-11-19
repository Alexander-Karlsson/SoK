using Bookings.Application.Bookings.DTOs.Request;

namespace Bookings.Application.Bookings.Validation;

public static class BookingValidation
{
    public const int MinRentalDays = 1;
    public const int MaxRentalDays = 30;

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
        if (endsAt is null)
            return;

        var length = endsAt.Value - startsAt;

        if (length < TimeSpan.FromDays(MinRentalDays))
            throw new ArgumentException($"En bokning måste vara minst {MinRentalDays} dag.");

        if (length > TimeSpan.FromDays(MaxRentalDays))
            throw new ArgumentException($"En bokning får vara högst {MaxRentalDays} dagar.");
    }

    private static void ValidateEmployees(List<Guid> employeeIds)
    {
        if (employeeIds.Contains(Guid.Empty))
            throw new ArgumentException("EmployeeIds innehåller ett ogiltigt id.");
    }

    private static void ValidateServices(List<CreateBookingServiceRequest> services)
    {
        if (services.Any(s => s.ServiceId == Guid.Empty || s.EstimatedHours < 0))
            throw new ArgumentException("Services innehåller ogiltiga värden.");

        if (services.DistinctBy(s => s.ServiceId).Count() != services.Count)
            throw new ArgumentException("Samma tjänst finns flera gånger.");
    }

    private static void ValidateMachines(List<CreateBookingMachineRequest> machines)
    {
        if (machines.Any(m => m.MachineId == Guid.Empty))
            throw new ArgumentException("Machines innehåller ogiltiga värden.");

        if (machines.DistinctBy(m => m.MachineId).Count() != machines.Count)
            throw new ArgumentException("Samma maskin finns flera gånger.");
    }
}

