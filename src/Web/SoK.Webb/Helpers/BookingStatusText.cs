using Bookings.Domain.Enums;

namespace SoK.Webb.Helpers;

public static class BookingStatusText
{
    public static string ToSwedish(this BookingStatus status) => status switch
    {
        BookingStatus.Booked => "Bokad",
        BookingStatus.Completed => "Avslutad",
        BookingStatus.Cancelled => "Avbokad",
        _ => status.ToString()
    };
}