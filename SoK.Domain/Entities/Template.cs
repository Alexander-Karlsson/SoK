using System;
using System.Collections.Generic;
using System.Text;

namespace SoK.Domain.Entities
{
    public class Template
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CompanyOrgNumber { get; set; } = string.Empty;
        public string VatNumber { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public string CustomerPostalCode { get; set; } = string.Empty;
        public string CustomerCity { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerBankTransfer { get; set; } = string.Empty;
        public string LogoPath { get; set; } = string.Empty;
        public string StandardText { get; set; } = string.Empty;
        public string PrimaryColor { get; set; } = string.Empty;
        public string TaxApprovalText { get; set; } = string.Empty;
    }
}
