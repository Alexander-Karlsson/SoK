using System;
using System.Collections.Generic;
using System.Text;

namespace SoK.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public string OcrReference { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool InvoiceStatus { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public Customer Customer { get; set; } = null!;
        public int CustomerId { get; set; }

        public Template Template { get; set; } = null!;
        public int TemplateId { get; set; }


    }
}
