using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public required string ProjectName { get; set; }
        public string WorkAddress { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public DateOnly? EndDate { get; set; }
    }
}
