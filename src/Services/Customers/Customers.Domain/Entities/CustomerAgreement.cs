using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Domain.Entities
{
    public class CustomerAgreement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public string Terms { get; set; } = string.Empty;
        public int PaymentTermsDays { get; set; } = 30;
        public DateOnly ValidFrom { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly? ValidTo { get; set; }

    }
}
