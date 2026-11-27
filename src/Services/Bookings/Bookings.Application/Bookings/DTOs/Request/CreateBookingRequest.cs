namespace Bookings.Application.Bookings.DTOs.Request;

public class CreateBookingRequest
{
    public Guid? ProjectId { get; set; }
    public required Guid CustomerId { get; set; }
    public required DateTime StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }

    public List<Guid> EmployeeIds { get; set; } = [];
    public List<CreateBookingServiceRequest> Services { get; set; } = [];
    public List<CreateBookingMachineRequest> Machines { get; set; } = [];

}