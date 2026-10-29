using System;
using System.Collections.Generic;
using System.Text;

namespace SoK.Domain.Entities
{
    internal class Customer
    {
        public int Id { get; set; }
        public Guid CustomerNumber { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string OrgNumber { get; set; } = string.Empty;
        public string contactPerson { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Oklart om behov finns. Vi låter stå kvar tills framtida eventuiella behov uppstår.
        //public string preferences { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public ICollection<Project> Projects = new List<Project>();
    }
}
