using System;
using System.Collections.Generic;
using System.Text;

namespace SoK.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string WorkAddress { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public Customer Customer { get; set; } = null!;
        public int CustomerId { get; set; }
    }
}
