using System;
using System.Collections.Generic;
using System.Text;

namespace SoK.Domain.Entities
{
    public class CustomerAgreement
    {
        public int Id { get; set; }
        public string Terms { get; set; } = string.Empty;
        public string PaymentTermsDays { get; set; } = string.Empty;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public Customer customer { get; set; } = null!;
        public int CustomerId { get; set; }
    }
}
