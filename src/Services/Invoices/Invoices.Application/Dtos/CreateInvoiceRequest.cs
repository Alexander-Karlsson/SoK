using System.ComponentModel.DataAnnotations;

namespace Invoices.Application.Dtos;

public class CreateInvoiceRequest
{
    public Guid CustomerId { get; set; }
    public Guid TemplateId { get; set; }

    [MinLength(1, ErrorMessage = "Fakturan måste ha minst en rad")]
    public List<CreateInvoiceLineRequest> Lines { get; set; } = [];

}
