using Invoices.Domain.Entities;

namespace Invoices.Application.Interfaces;

public interface ITemplateRepository
{
    Task<InvoiceTemplate> AddAsync(InvoiceTemplate template);
    Task<List<InvoiceTemplate>> GetAllAsync();
    Task<InvoiceTemplate> GetByIdAsync(Guid id);
}
