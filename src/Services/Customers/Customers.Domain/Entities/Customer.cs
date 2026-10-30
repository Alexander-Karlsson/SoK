using Customers.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string CustomerNumber { get; set; }

        public CustomerType CustomerType { get; set; }

        // Privatperson
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        // Företag
        public string? CompanyName { get; set; }
        public string? OrganisationNumber { get; set; }
        public string? ContactPerson { get; set; }

        // Gemensamma
        public required string Address { get; set; }
        public required string PostalCode { get; set; }
        public required string City { get; set; }

        public required string Phone { get; set; }
        public required string Email { get; set; }

        public string? Preferences { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
