using Invoices.Domain.Entities;

namespace Invoices.Application.Dtos;

public class UpdateInvoiceStatusRequest
{
    public InvoiceStatus Status { get; set; }
}
