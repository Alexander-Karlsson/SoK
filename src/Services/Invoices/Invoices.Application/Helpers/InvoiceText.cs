using Invoices.Domain.Entities;

namespace Invoices.Application.Helpers;

public static class InvoiceText
{
    public static string Status(InvoiceStatus status)
    {
        return status switch
        {
            InvoiceStatus.Draft => "Utkast",
            InvoiceStatus.Sent => "Skickad",
            InvoiceStatus.Paid => "Betald",
            InvoiceStatus.Overdue => "Förfallen",
            _ => status.ToString()
        };
    }

    public static string Unit(InvoiceUnit unit)
    {
        return unit switch
        {
            InvoiceUnit.Hours => "tim",
            InvoiceUnit.Days => "dagar",
            InvoiceUnit.Pieces => "st",
            _ => unit.ToString()
        };
    }
}