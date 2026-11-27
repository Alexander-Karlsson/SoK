using Bookings.Application.Bookings.DTOs.Invoices;

namespace Bookings.Application.Bookings.Interfaces;

public interface IInvoiceClient
{
    Task<Guid> CreateInvoiceAsync(InvoiceRequest request);
}