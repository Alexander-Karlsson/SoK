using Bookings.Application.Bookings.DTOs.Response;
using Bookings.Domain.Entities;

namespace Bookings.Application.Bookings.Mappings;

public static class BusinessServiceMappings
{
    public static BusinessServiceResponse ToResponse(this BusinessService service) => new(
        service.Id,
        service.Name,
        service.Description,
        service.PricePerHour,
        service.IsActive,
        service.RequiredTitle
    );
}