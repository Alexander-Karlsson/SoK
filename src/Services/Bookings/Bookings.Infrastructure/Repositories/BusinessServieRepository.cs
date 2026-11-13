using Bookings.Application.Bookings.Interfaces;
using Bookings.Domain.Entities;
using Bookings.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Repositories;

public class BusinessServieRepository(BookingDbContext context) : IBusinessServiceRepository
{
    public async Task<BusinessService> AddAsync(BusinessService service)
    {
        context.BusinessServices.Add(service);
        await context.SaveChangesAsync();
        return service;
    }

    public async Task DeleteAsync(BusinessService service)
    {
        context.BusinessServices.Remove(service);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<BusinessService>> GetAllAsync() => await context.BusinessServices
        .AsNoTracking()
        .OrderBy(s => s.Name)
        .ToListAsync();

    public async Task<BusinessService?> GetByIdAsync(Guid id) => await context.BusinessServices.FindAsync(id);

    public async Task<IReadOnlyList<BusinessService>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        var idList = ids.Distinct().ToList();
        return await context.BusinessServices
            .AsNoTracking()
            .Where(s => idList.Contains(s.Id))
            .ToListAsync(); 
    }

    public Task<bool> IsInUseAsync(Guid id) => context.BookingServices.AnyAsync(bm => bm.ServiceId == id);

    public async Task UpdateAsync(BusinessService service) => await context.SaveChangesAsync();

}
