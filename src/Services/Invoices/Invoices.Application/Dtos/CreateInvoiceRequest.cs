namespace Invoices.Application.Dtos;

public class CreateInvoiceRequest
{
    public Guid CustomerId { get; set; }
    public Guid TemplateId { get; set; }
    public List<CreateInvoiceLineRequest> Lines { get; set; } = [];

}
