using Invoices.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Invoices.Application.Dtos;

public class CreateInvoiceLineRequest
{
    [Required(ErrorMessage = "Får inte vara tomt")]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 999999999, 
        ErrorMessage = "Antalet måste vara större än 0")]
    public decimal Quantity { get; set; }

    public InvoiceUnit Unit { get; set; }

    [Range(0, 10000000, ErrorMessage = "Priset kan inte vara negativt")]
    public decimal UnitPrice { get; set; }

    //[Range(0, 0.25, ErrorMessage = "Momsen måste anges")]
    //public decimal VatRate { get; set; } = 0.25m;
}
