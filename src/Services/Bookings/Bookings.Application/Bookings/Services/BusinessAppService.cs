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
            Description = NormalizeText(request.Description),
            PricePerHour = request.PricePerHour,
            RequiredTitle = NormalizeText(request.RequiredTitle)
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
        service.Description = NormalizeText(request.Description);
        service.PricePerHour = request.PricePerHour;
        service.IsActive = request.IsActive;
        service.RequiredTitle = NormalizeText(request.RequiredTitle);

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

    private static string? NormalizeText(string? text)
        => string.IsNullOrWhiteSpace(text) ? null : text.Trim();
}