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

    // Färg på statusbadgen: grön = bokad, grå = avslutad, röd = avbokad
    public static string ToBadgeClass(this BookingStatus status) => status switch
    {
        BookingStatus.Booked => "admin-badge-active",
        BookingStatus.Completed => "booking-badge-completed",
        _ => "admin-badge-inactive"
    };
}