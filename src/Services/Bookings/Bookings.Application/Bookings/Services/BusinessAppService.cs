using Bookings.Application.Bookings.DTOs.Request;
using Bookings.Application.Bookings.Interfaces;
using Bookings.Application.Bookings.Validation;
using Bookings.Domain.Entities;

namespace Bookings.Application.Bookings.Services;

public class BusinessAppService(IBusinessServiceRepository repo)
{
    public async Task<BusinessService> CreateBusinessServiceAsync(CreateBusinessServiceRequest request)
    {
        BusinessServiceValidation.Validate(request.Name, request.PricePerHour);

        var service = new BusinessService
        {
            Name = request.Name.Trim(),
            Description = NormalizeDescription(request.Description),
            PricePerHour = request.PricePerHour
        };

        return await repo.AddAsync(service);
    }

    public async Task<BusinessService?> GetByIdAsync(Guid id)
        => await repo.GetByIdAsync(id);

    public async Task<IReadOnlyList<BusinessService>> GetAllAsync()
        => await repo.GetAllAsync();

    public async Task<bool> UpdateBusinessServiceAsync(Guid id, UpdateBusinessServiceRequest request)
    {
        BusinessServiceValidation.Validate(request.Name, request.PricePerHour);

        var service = await repo.GetByIdAsync(id);

        if (service is null)
            return false;

        service.Name = request.Name.Trim();
        service.Description = NormalizeDescription(request.Description);
        service.PricePerHour = request.PricePerHour;
        service.IsActive = request.IsActive;

        await repo.UpdateAsync(service);

        return true;
    }

    public async Task<bool> DeleteBusinessServiceAsync(Guid id)
    {
        var service = await repo.GetByIdAsync(id);

        if (service is null)
            return false;

        if (await repo.IsInUseAsync(id))
            throw new InvalidOperationException("Tjänsten finns i bokningar och kan inte tas bort. Inaktivera den i stället.");

        await repo.DeleteAsync(service);

        return true;
    }

    private static string? NormalizeDescription(string? description)
        => string.IsNullOrWhiteSpace(description) ? null : description.Trim();
}