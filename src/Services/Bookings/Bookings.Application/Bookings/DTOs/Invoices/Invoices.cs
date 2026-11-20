namespace Bookings.Application.Bookings.DTOs.Invoices;

public record InvoiceLineRequest(string Description, decimal Quantity, string Unit, decimal UnitPrice);

public record InvoiceRequest(Guid CustomerId, Guid TemplateId, List<InvoiceLineRequest> Lines);