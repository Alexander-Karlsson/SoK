using Bookings.Domain.Entities;

namespace Bookings.Application.Bookings.Interfaces;

public interface IBusinessServiceRepository
{
    Task<BusinessService?> GetByIdAsync(Guid id);
    //IEnumerable to get all the services when booking calls for it, if multiple services per booking
    Task<IReadOnlyList<BusinessService>> GetByIdsAsync(IEnumerable<Guid> ids);
    Task<BusinessService> AddAsync(BusinessService service);
    Task UpdateAsync(BusinessService service);
    Task DeleteAsync(BusinessService service);
    Task<IReadOnlyList<BusinessService>> GetAllAsync();
    Task<bool> IsInUseAsync(Guid id);
}
