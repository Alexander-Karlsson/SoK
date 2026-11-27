using Bookings.Application.Bookings.DTOs.Request;

namespace Bookings.Application.Bookings.Validation;

public static class BookingValidation
{
    public const int MinRentalDays = 1;
    public const int MaxRentalDays = 30;
    public const int HoursPerDay = 8;
    public const decimal MaxHoursPerDay = 24;

    public static void ValidateCreate(CreateBookingRequest request)
    {
        if (request.CustomerId == Guid.Empty)
            throw new ArgumentException("CustomerId saknas.");

        if (request.ProjectId == Guid.Empty)
            throw new ArgumentException("ProjectId är ogiltigt.");

        if (request.EmployeeIds.Count == 0 && request.Services.Count == 0 && request.Machines.Count == 0)
            throw new ArgumentException("Bokningen måste innehålla minst en anställd, tjänst eller maskin.");
       
        if (request.StartsAt.Date < DateTime.UtcNow.Date)
            throw new ArgumentException("En bokning kan inte starta bakåt i tiden.");

        ValidateDates(request.StartsAt, request.EndsAt);
        ValidateEmployees(request.EmployeeIds);
        ValidateServices(request.Services);
        ValidateMachines(request.Machines);

        var endsAt = request.EndsAt ?? request.StartsAt.AddDays(MaxRentalDays);
        ValidateServiceHours(request.StartsAt, endsAt, request.Services.Select(s => s.EstimatedHours));
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

    // En tjänst räknas som arbetstid: högst HoursPerDay timmar per påbörjad dag i perioden.
    // Dagarna räknas på samma sätt som på fakturan (Math.Ceiling).
    public static void ValidateServiceHours(DateTime startsAt, DateTime endsAt, IEnumerable<decimal> estimatedHours)
    {
        var days = (decimal)Math.Ceiling((endsAt - startsAt).TotalDays);
        var maxHours = days * HoursPerDay;

        if (estimatedHours.Any(h => h > maxHours))
            throw new ArgumentException($"En tjänst kan ha högst {maxHours:0} timmar för den här perioden ({days:0} dagar × {HoursPerDay} timmar).");
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

